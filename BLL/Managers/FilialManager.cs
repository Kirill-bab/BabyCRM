using DAL.DbManagers;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class FilialManager : EntityManager<Filial>
    {
        public FilialManager(IDbManager<Filial> dbManager) : base(dbManager)
        {
        }

        public override string ProcedurePrefix => "[Filial].Filial";

        public override async Task<IEnumerable<Filial>> GetAll()
        {
            var filials = await _dbManager.LoadData<Filial, Client, dynamic>(
                "SELECT  f.*, c.Id as ClientId, c.FirstName, c.LastName, c.Birthday, c.ParentFullName, c.PhoneNumber, c.EmailAddress, c.SocialNetworks, c.CreatedDate, c.CreatedBy, c.DataVersion, c.FilialId\r\nFROM [Filial].Filial f LEFT JOIN [Client].Client c on f.Id = c.FilialId",
                (filial, client) =>
                {
                    filial.Clients ??= new List<Client>();
                    filial.Clients.Add(client);
                    return filial;
                }, new { }, "ClientId");
            var result = filials.GroupBy(f => f.Id).Select(g =>
            {
                var groupedFilial = g.First();
                groupedFilial.Clients = g.Select(f => f.Clients.Single()).ToList();
                return groupedFilial;
            }).ToList();                                    //adding ToList() call prevents second call to inner Select() statement. Otherwise, run fails on call to Single() function, because sequence contains more than one element. My guess is that IEnumerable interface only creates a sequence of actions that need to be done to collection rather than returning an actual result. 
                                                            // TODO: ask teacher a question about why this is happening... Bloody dark .net magic
            return result;                                  //Answer: Select() does not call provided delegate. It only construct deferred executed IEnumerable object. And delegate will be called each time you enumerate resulted IEnumerable object.
        }

        public override async Task<Filial?> Get(int id)
        {
            var result = await _dbManager.LoadData($"{ProcedurePrefix}_Get", new { Id = id });
            return result.FirstOrDefault();
        }

        public override async Task<IEnumerable<Filial>> GetByAuthor(string author)
        {
            return await _dbManager.LoadData($"{ProcedurePrefix}_GetByAuthor", new { Author = author });
        }

        public override async Task Add(Filial filial)
        {
            await _dbManager.ExecuteProcedure($"{ProcedurePrefix}_Insert",
                new
                {
                    filial.Name,
                    filial.PhoneNumber,
                    filial.Address,
                    CreatedDate = DateTime.Now,
                    filial.CreatedBy,
                    DataVersion = 0
                });
        }
    }
}

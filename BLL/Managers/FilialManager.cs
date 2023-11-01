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
            }).ToList();
           return result;
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

using DAL.DbManagers;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Commands.Filials;

namespace BLL.Managers
{
    public class FilialManager : EntityManager<FilialDataModel, CreateFilialCommand, UpdateFilialCommand>
    {
        public FilialManager(IDbManager<FilialDataModel> dbManager) : base(dbManager)
        {
        }

        public override string ProcedurePrefix => "[Filial].Filial";

        public override async Task<IEnumerable<FilialDataModel>> GetAll()
        {
            var filials = await _dbManager.LoadData<FilialDataModel, ClientDataModel, dynamic>(
                @"SELECT  f.*, c.* 
                        FROM [Filial].Filial f LEFT JOIN [Client].Client c on f.Id = c.FilialId",
                (filial, client) =>
                {
                    filial.Clients ??= new List<ClientDataModel>();
                    filial.Clients.Add(client);
                    return filial;
                }, new { });
            var result = filials.GroupBy(f => f.Id).Select(g =>
            {
                var groupedFilial = g.First();
                groupedFilial.Clients = g.Select(f => f.Clients.Single()).ToList();
                return groupedFilial;
            }).ToList();                                    //adding ToList() call prevents second call to inner Select() statement. Otherwise, run fails on call to Single() function, because sequence contains more than one element. My guess is that IEnumerable interface only creates a sequence of actions that need to be done to collection rather than returning an actual result. 
                                                            // TODO: ask teacher a question about why this is happening... Bloody dark .net magic
            return result;                                  //Answer: Select() does not call provided delegate. It only construct deferred executed IEnumerable object. And delegate will be called each time you enumerate resulted IEnumerable object.
        }

        public override async Task<FilialDataModel?> Get(int id)
        {
            var filials = await _dbManager.LoadData<FilialDataModel, ClientDataModel, dynamic>(
                @"SELECT  f.*, c.* 
                        FROM [Filial].Filial f LEFT JOIN [Client].Client c on f.Id = c.FilialId
                        WHERE f.Id = @Id",
                (filial, client) =>
                {
                    filial.Clients ??= new List<ClientDataModel>();
                    filial.Clients.Add(client);
                    return filial;
                }, new { Id = id });
            var result = filials.GroupBy(f => f.Id).Select(g =>
            {
                var groupedFilial = g.First();
                groupedFilial.Clients = g.Select(f => f.Clients.Single()).ToList();
                return groupedFilial;
            }).ToList();

            return result.FirstOrDefault();
        }

        public override async Task<IEnumerable<FilialDataModel>> GetByAuthor(string author)
        {
            var filials = await _dbManager.LoadData<FilialDataModel, ClientDataModel, dynamic>(
                @"SELECT  f.*, c.*
                        FROM [Filial].Filial f LEFT JOIN [Client].Client c on f.Id = c.FilialId
                        WHERE f.CreatedBy = @Author",
                (filial, client) =>
                {
                    filial.Clients ??= new List<ClientDataModel>();
                    filial.Clients.Add(client);
                    return filial;
                }, new { Author = author });
            var result = filials.GroupBy(f => f.Id).Select(g =>
            {
                var groupedFilial = g.First();
                groupedFilial.Clients = g.Select(f => f.Clients.Single()).ToList();
                return groupedFilial;
            }).ToList();

            return result;
        }
    }
}

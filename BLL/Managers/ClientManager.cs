using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbManagers;
using DAL.Entities;

namespace BLL.Managers
{
    public class ClientManager : IEntityManager<Client>
    {
        private readonly IDbManager<Client> _dbManager;

        public ClientManager(IDbManager<Client> dbManager)
        {
            _dbManager = dbManager;
        }

        public string ProcedurePrefix => "[Client].Client";

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _dbManager.LoadData($"{ProcedurePrefix}_GetAll", new { });
        }

        public async Task<Client?> Get(int id)
        {
            var result = await _dbManager.LoadData<dynamic>($"{ProcedurePrefix}_Get", new { Id = id });
            return result.FirstOrDefault();
        }

        public Task<IEnumerable<Client>> GetByAuthor(string author)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Client client)
        {
            await _dbManager.Insert(client);
        }
    }
}

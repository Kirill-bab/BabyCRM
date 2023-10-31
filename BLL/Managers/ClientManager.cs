using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbManagers;
using DAL.Entities;

namespace BLL.Managers
{
    public class ClientManager : EntityManager<Client>
    {
        public ClientManager(IDbManager<Client> dbManager) : base(dbManager)
        {
        }

        public override string ProcedurePrefix => "[Client].Client";

        public override async Task Add(Client client)
        {
            await _dbManager.ExecuteProcedure($"{ProcedurePrefix}_Insert",
                new
                {
                    client.FirstName,
                    client.LastName,
                    client.Birthday,
                    client.ParentFullName,
                    client.PhoneNumber,
                    client.EmailAddress,
                    client.SocialNetworks,
                    CreatedDate = DateTime.Now,
                    client.CreatedBy,
                    DataVersion = 0,
                    client.FilialId
                });
        }
    }
}

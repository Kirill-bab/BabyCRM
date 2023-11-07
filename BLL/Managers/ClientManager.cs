using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Commands;
using BLL.Commands.Clients;
using DAL.DbManagers;
using DAL.Models;
using DAL.Helpers;
using DAL.Models;
using Dapper;

namespace BLL.Managers
{
    public class ClientManager : EntityManager<ClientDataModel, CreateClientCommand, UpdateClientCommand>
    {
        public ClientManager(IDbManager<ClientDataModel> dbManager) : base(dbManager)
        {
        }

        public override string ProcedurePrefix => "[Client].Client";
        public async Task GenerateClients(int quantity)
        {
            var clients = DataGenerator.GenerateClients(quantity);
            await AddMany(clients);
        }

    }
}

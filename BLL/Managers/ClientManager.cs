using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Commands;
using BLL.Commands.Clients;
using DAL.Models;
using DAL.Helpers;
using DAL.Models;
using DAL.Repositories;
using Dapper;

namespace BLL.Managers
{
    public class ClientManager : EntityManager<ClientDataModel, CreateClientCommand, UpdateClientCommand>
    {
        public ClientManager(IRepository<ClientDataModel> repository) : base(repository)
        {
        }
        public async Task GenerateClients(int quantity)
        {
            var clients = DataGenerator.GenerateClients(quantity);
            await AddMany(clients);
        }
    }
}

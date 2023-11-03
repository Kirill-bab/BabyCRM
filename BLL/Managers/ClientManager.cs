using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Commands;
using BLL.Commands.Clients;
using DAL.DbManagers;
using DAL.Entities;
using Dapper;

namespace BLL.Managers
{
    public class ClientManager : EntityManager<ClientDataModel, CreateClientCommand, UpdateClientCommand>
    {
        public ClientManager(IDbManager<ClientDataModel> dbManager) : base(dbManager)
        {
        }

        public override string ProcedurePrefix => "[Client].Client";
        
    }
}

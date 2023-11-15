using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class ClientRepository : Repository<ClientDataModel>
    {
        public ClientRepository(ILoggerFactory loggerFactory, ISqlConnectionFactory connectionFactory) : base(loggerFactory, connectionFactory)
        {
        }

        public override string ProcedurePrefix => "[Client].Client";
    }
}

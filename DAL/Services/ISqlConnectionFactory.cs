using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface ISqlConnectionFactory
    {
        public IDbConnection CreateConnection();
        public IDbConnection CreateConnection(string connectionString);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.DbManagers
{
    public class ClientDbManager : DbManager<Client>
    {

        public ClientDbManager(string connectionString, string tableName) : base(connectionString, tableName)
        {
        }


        public override void Insert(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
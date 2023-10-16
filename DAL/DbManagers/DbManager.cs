using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Dapper;

namespace DAL.DbManagers
{
    public abstract class DbManager<T> where T : IDbEntity
    {
        private readonly string _tableName;
        private readonly string _connectionString;
        public abstract string ProcedurePrefix { get; set; }

        public DbManager(string connectionString, string tableName)
        {
            (_connectionString, _tableName) = (connectionString, tableName);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>($"select * from {_tableName}");  // TODO: move all queries to db procedures
            }
        }

        /*public IEnumerable<T> GetCreatedInInterval(DateTime from, DateTime to)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                return connection.Query<T>($"select * from {_tableName} where CreatedDate in ({},{})");
            }
        }*/

        public abstract void Insert(T entity);

        public IEnumerable<T> GetCreatedBy(string author)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                return connection.Query<T>($"select * from {_tableName} where CreatedBy = '{author}'");
            }
        }
    }
}

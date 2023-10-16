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
    public class DbManager<T> : IDbManager<T> where T : IDbEntity
    {
        private readonly string _connectionString;

        public DbManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> LoadData <U>(string procedureName, U parameters)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(_connectionString);
            return await connection.QueryAsync<T>(procedureName, parameters,
                commandType: CommandType.StoredProcedure); // TODO: create db procedures
        }

        public async Task Insert(T entity)
        {
        }
    }
}

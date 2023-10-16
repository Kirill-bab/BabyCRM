using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DAL.DbManagers
{
    public class DbManager<T> : IDbManager<T> where T : IDbEntity
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public DbManager(string connectionString, ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
            _logger = loggerFactory.CreateLogger<DbManager<T>>();
        }

        public async Task<IEnumerable<T>> LoadData <U>(string procedureName, U parameters)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);
                return await connection.QueryAsync<T>(procedureName, parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                _logger.LogWarning(ex,"There was an error with loading data from a database");
            }

            return new List<T>();
        }

        public async Task Insert(T entity)
        {
        }
    }
}

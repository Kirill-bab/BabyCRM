using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DAL.DbManagers
{
    public class DbManager<T> : IDbManager<T> where T : IDbEntity
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public DbManager(ILoggerFactory loggerFactory, IConfiguration config)
        {
            //This code is for retrieving CN from app.config file
            //_connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            _connectionString = config.GetConnectionString("Default");
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
            catch (SqlException)
            {
                _logger.LogWarning("There was an error with loading data from a database");
            }

            return new List<T>();
        }

        public async Task ExecuteProcedure<U>(string procedureName, U parameters)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(procedureName, parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                _logger.LogWarning($"There was an error executing procedure {procedureName} on a database");
            }
        }
    }
}

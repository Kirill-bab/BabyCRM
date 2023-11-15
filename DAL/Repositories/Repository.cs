using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Models;
using DAL.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Z.Dapper.Plus;

namespace DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : IDbEntity
    {
        protected readonly ISqlConnectionFactory ConnectionFactory;
        protected readonly ILogger Logger;
        public abstract string ProcedurePrefix { get; }

        protected Repository(ILoggerFactory loggerFactory, ISqlConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
            Logger = loggerFactory.CreateLogger<Repository<T>>();
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                return await connection.QueryAsync<T>($"{ProcedurePrefix}_GetAll", new { },
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                Logger.LogWarning("There was an error with retrieving data from a database: " + ex.Message);
            }

            return new List<T>();
        }

        public virtual async Task<T> GetOne(int id)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                return await connection.QuerySingleAsync<T>($"{ProcedurePrefix}_Get", new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Logger.LogWarning("There was an error with retrieving data from a database: " + ex.Message);
            }

            return default;
        }

        public virtual async Task Save<TCreateCommand>(TCreateCommand command)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                await connection.ExecuteAsync($"{ProcedurePrefix}_Insert", command,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                Logger.LogWarning($"There was an error executing procedure {ProcedurePrefix}_Insert on a database: " + ex.Message);
            }
        }

        public virtual async Task SaveMany(IEnumerable<T> entities)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                await connection.BulkActionAsync(c => c.BulkInsert(entities));
            }
            catch (SqlException ex)
            {
                Logger.LogWarning($"There was an error with saving entities of type {typeof(T)} to a database: " + ex.Message);
            }
        }

        public virtual async Task Update<TUpdateCommand>(TUpdateCommand command)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                await connection.ExecuteAsync($"{ProcedurePrefix}_Update", command,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                Logger.LogWarning($"There was an error executing procedure {ProcedurePrefix}_Update on a database: " + ex.Message);
            }
        }

        public virtual async Task Delete(int id)
        {
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                await connection.ExecuteAsync($"{ProcedurePrefix}_Delete", new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                Logger.LogWarning($"There was an error executing procedure {ProcedurePrefix}_Delete on a database: " + ex.Message);
            }
        }
    }
}

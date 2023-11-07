﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Z.Dapper.Plus;

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
            catch (SqlException ex)
            {
                _logger.LogWarning("There was an error with loading data from a database: " + ex.Message);
            }

            return new List<T>();
        }

        public async Task<IEnumerable<T>> LoadData<TFirst, TSecond, U>
            (string sql,
                Func<TFirst, TSecond, T> map,
                U parameters,
                string splitOn = "Id")
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);
                return await connection.QueryAsync<TFirst ,TSecond, T>(sql, map, parameters, splitOn: splitOn);
            }
            catch (SqlException ex)
            {
                _logger.LogWarning("There was an error with loading data from a database: " + ex.Message);
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
            catch (SqlException ex)
            {
                _logger.LogWarning($"There was an error executing procedure {procedureName} on a database: " + ex.Message);
            }
        }

        public async Task SaveMany(IEnumerable<T> entities)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);
                await connection.BulkActionAsync(c => c.BulkInsert(entities));
            }
            catch (SqlException ex)
            {
                _logger.LogWarning($"There was an error with saving entities of type {typeof(T)} to a database: " + ex.Message);
            }
        }
    }
}

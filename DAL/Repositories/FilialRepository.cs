using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class FilialRepository : Repository<FilialDataModel>
    {
        public FilialRepository(ILoggerFactory loggerFactory, ISqlConnectionFactory connectionFactory) : base(loggerFactory, connectionFactory)
        {
        }

        public override string ProcedurePrefix => "[Filial].Filial";

        public override async Task<IEnumerable<FilialDataModel>> Get()
        {
            Dictionary<int, FilialDataModel> filialsDictionary = new();
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();

                await connection.QueryAsync<FilialDataModel, ClientDataModel, FilialDataModel>(
                    $"{ProcedurePrefix}_GetAll",
                    (filial, client) =>
                    {
                        if (filialsDictionary.TryGetValue(filial.Id, out var existingFilial))
                        {
                            filial = existingFilial;
                        }
                        else
                        {
                            filialsDictionary.Add(filial.Id, filial);
                        }

                        filial.Clients ??= new List<ClientDataModel>();
                        filial.Clients.Add(client);

                        return filial;
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                Logger.LogWarning("There was an error with retrieving data from a database: " + ex.Message);
            }

            return filialsDictionary.Values;
        }

        public override async Task<FilialDataModel> GetOne(int id)
        {
            Dictionary<int, FilialDataModel> filialsDictionary = new();
            try
            {
                using IDbConnection connection = ConnectionFactory.CreateConnection();

                await connection.QueryAsync<FilialDataModel, ClientDataModel, FilialDataModel>(
                    $"{ProcedurePrefix}_Get",
                    (filial, client) =>
                    {
                        if (filialsDictionary.TryGetValue(filial.Id, out var existingFilial))
                        {
                            filial = existingFilial;
                        }
                        else
                        {
                            filialsDictionary.Add(filial.Id, filial);
                        }

                        filial.Clients ??= new List<ClientDataModel>();
                        filial.Clients.Add(client);

                        return filial;
                    },
                    new { FilialId = id },
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                Logger.LogWarning("There was an error with retrieving data from a database: " + ex.Message);
            }

            return filialsDictionary.TryGetValue(id, out var filial) ? filial : null;
        }
    }
}

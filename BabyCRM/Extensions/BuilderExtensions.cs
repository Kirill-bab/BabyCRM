using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using BLL.Commands.Clients;
using BLL.Commands.Filials;
using BLL.Managers;
using DAL.Models;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using DAL;
using DAL.Repositories;
using DAL.Services;
using Dapper;
using Microsoft.AspNetCore.Hosting.Server;

namespace BabyCRM.Extensions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            //Add Repositories
            builder.Services.AddSingleton<IRepository<ClientDataModel>, ClientRepository>();
            builder.Services.AddSingleton<IRepository<FilialDataModel>, FilialRepository>();
            //Add Managers
            builder.Services.AddSingleton<EntityManager<ClientDataModel, CreateClientCommand, UpdateClientCommand>, ClientManager>();
            builder.Services.AddSingleton<EntityManager<FilialDataModel, CreateFilialCommand, UpdateFilialCommand>, FilialManager>();

            builder.Services.AddSingleton<ISqlConnectionFactory>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("Default") ??
                                       throw new ApplicationException("The connection string is null");

                return new SqlConnectionFactory(connectionString);
            });
            builder.AddMigrations();
            return builder;
        }

        private static void AddMigrations(this WebApplicationBuilder builder)
        {
            using var serviceProvider = new ServiceCollection().AddFluentMigratorCore().ConfigureRunner(config => config
                    .AddSqlServer()
                    .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
                    .ScanIn(Assembly.GetAssembly(typeof(ClientDataModel))).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole()).BuildServiceProvider(false);

            using var scope = serviceProvider.CreateScope();

            using IDbConnection connection = new SqlConnection(builder.Configuration.GetConnectionString("Setup"));

            var mainDbUpScript = File.ReadAllText(@"../DAL/Migrations/Scripts/MainDbUP.sql");
            connection.Execute(mainDbUpScript);

            if (builder.Environment.IsDevelopment())
            {
                var testDbUpScript = File.ReadAllText(@"../DAL/Migrations/Scripts/TestDbUP.sql");
                connection.Execute(testDbUpScript);
            }

            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}

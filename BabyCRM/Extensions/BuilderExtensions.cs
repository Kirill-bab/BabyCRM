using System.Data.SqlClient;
using System.Reflection;
using BLL.Commands.Clients;
using BLL.Commands.Filials;
using BLL.Managers;
using DAL.DbManagers;
using DAL.Models;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using DAL;
using Dapper;
using Microsoft.AspNetCore.Hosting.Server;

namespace BabyCRM.Extensions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IDbManager<ClientDataModel>, DbManager<ClientDataModel>>();
            builder.Services.AddSingleton<EntityManager<ClientDataModel, CreateClientCommand, UpdateClientCommand>, ClientManager>();
            builder.Services.AddSingleton<IDbManager<FilialDataModel>, DbManager<FilialDataModel>>();
            builder.Services.AddSingleton<EntityManager<FilialDataModel, CreateFilialCommand, UpdateFilialCommand>, FilialManager>();
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

            using var connection = new SqlConnection(builder.Configuration.GetConnectionString("Setup"));
            var mainDbUpScript = File.ReadAllText(@"../DAL/Migrations/Scripts/MainDbUP.sql");
            var testDbUpScript = File.ReadAllText(@"../DAL/Migrations/Scripts/TestDbUP.sql");
            connection.Execute(mainDbUpScript);
            connection.Execute(testDbUpScript);

            using var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}

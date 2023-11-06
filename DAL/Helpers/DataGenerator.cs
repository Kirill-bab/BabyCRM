using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL.Helpers
{
    public static class DataGenerator
    {
        public static async Task GenerateClients(IConfiguration config,int quantity)
        {
            string[] clientNames = {"James", "John", "Teodor", "Mike", "Kate", "Linda", "Karl", "Marge", "Sonic", "Amy" };
            string[] clientLastNames = { "Bush", "Trump", "Vasovsky", "Linkoln", "Simpson", "Peterson", "Griffin", "Marge", "von Bruck", "Vayner" };

            using var connection = new SqlConnection(config.GetConnectionString("Default"));
            var table = new DataTable();
            table.Columns.Add("Id");
            table.Columns.Add("FirstName");
            table.Columns.Add("LastName");
            table.Columns.Add("Birthday");
            table.Columns.Add("ParentFullName");
            table.Columns.Add("PhoneNumber");
            table.Columns.Add("EmailAddress");
            table.Columns.Add("SocialNetworks");
            table.Columns.Add("CreatedDate");
            table.Columns.Add("CreatedBy");
            table.Columns.Add("DataVersion");
            table.Columns.Add("FilialId");

            for (int i = 0; i < quantity; i++)
            {
                var (firstName, lastName) = (clientNames.SelectRandom(), clientLastNames.SelectRandom());

                table.Rows.Add(new object[]
                {
                    i + 1,
                    firstName,
                    lastName,
                    new DateTime(Random.Shared.Next(1900, 2023), Random.Shared.Next(1, 12), Random.Shared.Next(1, 30)),
                    $"{clientNames.SelectRandom()} {lastName}",
                    $"+380{Random.Shared.Next(100000000, 999999999)}",
                    $"{firstName}{lastName}@gmail.com".ToLower(),
                    "SGDCVABYCGByc",
                    DateTime.Now,
                    "Admin",
                    0,
                    null
                });
            }

            using var bulkCopy = new SqlBulkCopy(connection);
            bulkCopy.DestinationTableName = "[Client].[Client]";
            bulkCopy.BatchSize = 100;
            bulkCopy.BulkCopyTimeout = 30;
            connection.Open();
            await bulkCopy.WriteToServerAsync(table);
        }

        private static string SelectRandom(this string[] array)
        {
            return array[Random.Shared.Next(0, array.Length - 1)];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL.Helpers
{
    public static class DataGenerator
    {
        public static IEnumerable<ClientDataModel> GenerateClients(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("quantity must be a positive number!", nameof(quantity));

            string[] clientNames = {"James", "John", "Teodor", "Mike", "Kate", "Linda", "Karl", "Marge", "Sonic", "Amy" };
            string[] clientLastNames = { "Bush", "Trump", "Vasovsky", "Linkoln", "Simpson", "Peterson", "Griffin", "Marge", "von Bruck", "Vayner" };

            var clients = new List<ClientDataModel>(quantity);

            for (int i = 0; i < quantity; i++)
            {
                var (firstName, lastName) = (clientNames.SelectRandom(), clientLastNames.SelectRandom());
                clients.Add(new ClientDataModel
                {
                    Id = i + 1,
                    FirstName = firstName,
                    LastName = lastName,
                    Birthday = new DateTime(Random.Shared.Next(1900, 2023), Random.Shared.Next(1, 12),
                        Random.Shared.Next(1, 30)),
                    ParentFullName = $"{clientNames.SelectRandom()} {lastName}",
                    PhoneNumber = $"+380{Random.Shared.Next(100000000, 999999999)}",
                    EmailAddress = $"{firstName}{lastName}@gmail.com".ToLower(),
                    SocialNetworks = "SGDCVABYCGByc",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    DataVersion = 0,
                    FilialId = null
                });
            }

            return clients;
        }

        private static string SelectRandom(this string[] array)
        {
            return array[Random.Shared.Next(0, array.Length - 1)];
        }
    }
}

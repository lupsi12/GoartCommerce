using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace Domain.Context
{
    public class ApplicationDbContextFactory
    {
        public ApplicationDbContext CreateMongoDbContext()
        {
            // var homeDbConnectionString = Environment.GetEnvironmentVariable("homedb_connectionstring");
            //
            // if (string.IsNullOrEmpty(homeDbConnectionString))
            // {
            //     throw new InvalidOperationException("The environment variable 'homeDbConnectionString' is not set.");
            // }
          
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MongoDbConnection");
            var databaseName = configuration["MongoDbSettings:DatabaseName"];

            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName))
            {
                throw new InvalidOperationException("The connection string or database name is not set.");
            }

            return new ApplicationDbContext(connectionString, databaseName);
        }
    }
}

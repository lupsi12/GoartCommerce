using System;
using Domain.Configuration;
using Domain.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Domain.Context
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _database;

        public ApplicationDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            //CampaignConfiguration.RegisterClassMaps();
        }

        public IMongoCollection<T> GetCollection<T>() where T : class => _database.GetCollection<T>(typeof(T).Name);
    }
}

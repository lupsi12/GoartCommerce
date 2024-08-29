using System;
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
        }

        public IMongoCollection<T> GetCollection<T>() where T : class => _database.GetCollection<T>(typeof(T).Name);
    }
}

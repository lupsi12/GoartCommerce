using Core.MongoRepositories;
using MongoDB.Driver;

namespace Domain.Context
{


    public class ApplicationDbContextAdapter : IMongoDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public ApplicationDbContextAdapter(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _mongoDatabase.GetCollection<T>(name);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync<T>(string collectionName, T entity)
        {
            var collection = GetCollection<T>(collectionName);
            await collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var collection = GetCollection<T>(collectionName);
            await collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync<T>(string collectionName, FilterDefinition<T> filter)
        {
            var collection = GetCollection<T>(collectionName);
            await collection.DeleteOneAsync(filter);
        }
    }
}
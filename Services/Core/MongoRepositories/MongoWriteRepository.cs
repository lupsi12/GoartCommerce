using Core.Shared.EntityBase;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.MongoRepositories
{
    public class MongoWriteRepository<T> : IMongoWriteRepository<T> where T : class, IEntityBase, new()
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MongoWriteRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        private IMongoCollection<T> Collection => _mongoDbContext.GetCollection<T>(typeof(T).Name);

        public async Task AddAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await Collection.InsertManyAsync(entities);
        }

        public async Task UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            await Collection.UpdateOneAsync(filter, update);
        }

        public async Task HardDeleteAsync(FilterDefinition<T> filter)
        {
            await Collection.DeleteOneAsync(filter);
        }

        public async Task HardDeleteRangeAsync(FilterDefinition<T> filter)
        {
            await Collection.DeleteManyAsync(filter);
        }
    }
}
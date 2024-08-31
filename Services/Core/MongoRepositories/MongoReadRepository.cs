using Core.Shared.EntityBase;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.MongoRepositories
{
    public class MongoReadRepository<T> : IMongoReadRepository<T> where T : class, IEntityBase, new()
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MongoReadRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        private IMongoCollection<T> Collection => _mongoDbContext.GetCollection<T>(typeof(T).Name);

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null
                ? await Collection.Find(Builders<T>.Filter.Empty).ToListAsync()
                : await Collection.Find(predicate).ToListAsync();
        }

        public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null, int currentPage = 1, int pageSize = 3)
        {
            return predicate == null
                ? await Collection.Find(Builders<T>.Filter.Empty).Skip((currentPage - 1) * pageSize).Limit(pageSize).ToListAsync()
                : await Collection.Find(predicate).Skip((currentPage - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null
                ? (int)await Collection.CountDocumentsAsync(Builders<T>.Filter.Empty)
                : (int)await Collection.CountDocumentsAsync(predicate);
        }

        public async Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return (await Collection.FindAsync(predicate)).ToEnumerable().AsQueryable();
        }
    }
}

using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Core.MongoRepositories
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task InsertAsync<T>(string collectionName, T entity);
        Task UpdateAsync<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update);
        Task DeleteAsync<T>(string collectionName, FilterDefinition<T> filter);
    }
}
using Core.Shared.EntityBase;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Core.MongoRepositories
{
    public interface IMongoWriteRepository<T> where T : class, IEntityBase, new()
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IList<T> entities);
        Task UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);
        Task HardDeleteAsync(FilterDefinition<T> filter);
        Task HardDeleteRangeAsync(FilterDefinition<T> filter);
    }
}
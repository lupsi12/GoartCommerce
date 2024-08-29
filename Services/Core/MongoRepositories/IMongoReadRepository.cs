using Core.Shared.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.MongoRepositories
{
    public interface IMongoReadRepository<T> where T : class, IEntityBase, new()
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null, int currentPage = 1, int pageSize = 3);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
        Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
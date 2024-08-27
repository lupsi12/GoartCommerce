using Core.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Context
{
    public class ApplicationDbContextAdapter : IDbContext
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextAdapter(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Set<T>() where T : class => _context.Set<T>();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);
        public DbSet<TEntity> Set<TEntity>(string name) where TEntity : class => _context.Set<TEntity>(name);
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class => _context.Entry(entity);
    }
}
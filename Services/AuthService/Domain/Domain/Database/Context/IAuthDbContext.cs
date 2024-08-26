using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Context;

public interface IAuthDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Supplier> Suppliers { get; set; }
    Task<int> SaveChangesAsync();
    int SaveChanges();
}
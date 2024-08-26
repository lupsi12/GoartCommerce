using System.Reflection;
using Database.Configuration;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Context;

public class AuthDbContext : DbContext, IAuthDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return base.SaveChanges();
    }
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
     {
     }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder);
    //     optionsBuilder.UseMySql(
    //         "Server=mysql-143a5c3b-kcanmersin-7b3a.d.aivencloud.com;Port=12996;Database=defaultdb;User=avnadmin;Password=AVNS_1EyHJ73_ZRoboVIyyPB;SslMode=Required;"
    //         ,new
    //             MySqlServerVersion(
    //                 new
    //                     Version(8, 0, 21)));
    //     //optionsBuilder.UseSqlServer("Server=mysql-143a5c3b-kcanmersin-7b3a.d.aivencloud.com;Database=defaultdb;User=avnadmin;Password=AVNS_1EyHJ73_ZRoboVIyyPB; TrustServerCertificate=true;");
    //     //optionsBuilder.UseSqlServer("Server=localhost; Database=AuthDb;User Id=sa;Password=Kefal09sl@; TrustServerCertificate=true");
    //         // var connectionString = _configuration.GetConnectionString("DefaultConnection");
    //         // optionsBuilder.UseSqlServer(connectionString);
    // }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new UserConfiguration());
        //modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
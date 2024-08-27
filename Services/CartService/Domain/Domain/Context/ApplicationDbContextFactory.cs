using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Domain.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var cartDbConnectionString = Environment.GetEnvironmentVariable("CARTDB_CONNECTIONSTRING");

            if (string.IsNullOrEmpty(cartDbConnectionString))
            {
                throw new InvalidOperationException("The environment variable 'CARTDB_CONNECTIONSTRING' is not set.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(cartDbConnectionString);  

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
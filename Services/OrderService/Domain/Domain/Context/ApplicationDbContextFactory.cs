using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Domain.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var orderDbConnectionString = Environment.GetEnvironmentVariable("orderdb_connectionstring");

            if (string.IsNullOrEmpty(orderDbConnectionString))
            {
                throw new InvalidOperationException("The environment variable 'orderdb_connectionstring' is not set.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(orderDbConnectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

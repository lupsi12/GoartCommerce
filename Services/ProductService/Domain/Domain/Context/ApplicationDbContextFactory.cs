using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Domain.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var productDbConnectionString = Environment.GetEnvironmentVariable("productdb_connectionstring");

            if (string.IsNullOrEmpty(productDbConnectionString))
            {
                throw new InvalidOperationException("The environment variable 'productdb_connectionstring' is not set.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(productDbConnectionString, ServerVersion.AutoDetect(productDbConnectionString));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

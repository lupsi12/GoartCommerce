using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Domain.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var authDbConnectionString = Environment.GetEnvironmentVariable("authdb_connectionstring");

            if (string.IsNullOrEmpty(authDbConnectionString))
            {
                throw new InvalidOperationException("The environment variable 'authdb_connectionstring' is not set.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(authDbConnectionString, ServerVersion.AutoDetect(authDbConnectionString));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

public static class Installer
{
    public static IServiceCollection InstallDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<AuthDbContext>(
        //     db => db.UseSqlServer(configuration.GetConnectionString("MssqlConnectionString")));


var
connectionString = Environment.GetEnvironmentVariable(
"ConnectionStrings__DefaultConnection"
);
            services.AddDbContext<AuthDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));



        services.AddScoped<IAuthDbContext, AuthDbContext>();

        return services;
    }
}
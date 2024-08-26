using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

public static class Installer
{
    public static IServiceCollection InstallDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var
        connectionString = Environment.GetEnvironmentVariable(
        "authdb_connection_string"
        );
        services.AddDbContext<AuthDbContext>(options =>
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));



        services.AddScoped<IAuthDbContext, AuthDbContext>();

        return services;
    }
}
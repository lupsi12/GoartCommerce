using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Common;
using Repository.Supplier;
using Repository.User;

namespace Repository;

public static class Installer
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<ISupplierRepo, SupplierRepo>();

        return services;
    }
}
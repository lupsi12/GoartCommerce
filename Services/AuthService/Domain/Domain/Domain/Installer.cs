using Domain.Common;
using Domain.Supplier;
using Domain.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class Installer
{
    public static IServiceCollection AddDomainService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISupplierService, SupplierService>();

        return services;
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class Installer
{
    public static IServiceCollection AddCqs(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddScoped();
        return services;
    }
}
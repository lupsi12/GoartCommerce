using System.Reflection;
using Application.Messages.Command.User;
using Application.Messages.Query.User;
using Core;
using Database;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace Application;

public static class Installer
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    { 
        //services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(CreateUserCommand).Assembly, typeof(UserQuery).Assembly);
        //services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);
        services.AddCqs(configuration);
        services.AddRepositories(configuration);
        services.AddDomainService(configuration);
        services.InstallDatabase(configuration);

        return services;
    }
    
}
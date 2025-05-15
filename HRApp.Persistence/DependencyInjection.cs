

using HRApp.Application.Interfaces;
using HRApp.Persistence.InMemory;
using HRApp.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HRApp.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddSingleton<InMemoryDbContext>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        
        return services;
    }
}


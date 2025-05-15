using HRApp.Application.Interfaces;
using HRApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HRApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        
        return services;
    }
}



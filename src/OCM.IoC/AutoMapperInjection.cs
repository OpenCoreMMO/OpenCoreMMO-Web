using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OCM.IoC;

public static class AutoMapperInjection
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, Assembly assembly)
    {
        services.AddAutoMapper(assembly);
        return services;
    }
}
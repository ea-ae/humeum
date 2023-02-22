using System.Reflection;

using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Configures application services.
/// </summary>
public static class ConfigureServices {
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services) {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}

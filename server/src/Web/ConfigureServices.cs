using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

using Web.Common;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Configures web services.
/// </summary>
public static class ConfigureServices {
    /// <summary>
    /// Run as an extension method from the main <see cref="Program.Program"/> method.
    /// </summary>
    /// <param name="services">Provided service collection.</param>
    /// <returns>Given service collection for further extension method chaining.</returns>
    public static IServiceCollection ConfigureWebServices(this IServiceCollection services) {
        services.AddControllers(o => {
            o.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(o => {
            // add xml comment docs to swagger
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(o => {
            // suppress ApiController custom binding, allowing multiple sources per complex object
            o.SuppressInferBindingSourcesForParameters = true;
        });

        return services;
    }
}

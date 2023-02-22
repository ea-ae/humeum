using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;

using Web.Common;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
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

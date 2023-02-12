using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

using Web.Common;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
    public static IServiceCollection ConfigureWebServices(this IServiceCollection services) {
        services.AddControllers(o => {
            o.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(o => {
            // suppress ApiController custom binding, allowing multiple sources per complex object
            o.SuppressInferBindingSourcesForParameters = true;
        });

        return services;
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
    public static IServiceCollection ConfigureWebServices(this IServiceCollection services) {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.Configure<ApiBehaviorOptions>(o => {
            // suppress ApiController custom binding, allowing multiple sources per complex object
            o.SuppressInferBindingSourcesForParameters = true;
        });

        return services;
    }
}

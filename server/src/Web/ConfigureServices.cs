using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

using Web;
using Web.Common;
using Web.Controllers;

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

        services.AddApiVersioning(o => {
            o.ReportApiVersions = true;
            o.RouteConstraintName = "ApiVersion";

            o.Conventions.Controller<AssetsController>().HasApiVersion(1, 0);
            o.Conventions.Controller<ProfilesController>().HasApiVersion(1, 0);
            o.Conventions.Controller<TaxSchemesController>().HasApiVersion(1, 0);
            o.Conventions.Controller<TransactionCategoriesController>().HasApiVersion(1, 0);
            o.Conventions.Controller<TransactionsController>().HasApiVersion(1, 0);
            o.Conventions.Controller<UsersController>().HasApiVersion(1, 0);
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerDocument();

        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(o => {
            // suppress ApiController custom binding, allowing multiple sources per complex object
            o.SuppressInferBindingSourcesForParameters = true;
        });

        return services;
    }
}

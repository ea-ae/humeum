using Application.Common.Interfaces;

using Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services) {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(appDataPath, "humeum.sqlite");

        services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

        return services;
    }
}

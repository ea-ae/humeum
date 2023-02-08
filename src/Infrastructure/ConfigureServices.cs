using Application.Common.Interfaces;

using Infrastructure.Common;
using Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
                                                                     IConfiguration config) {
        var dbSettingsSection = config.GetSection(nameof(DatabaseSettings));
        var dbSettings = dbSettingsSection.Get<DatabaseSettings>()!;

        services.Configure<DatabaseSettings>(dbSettingsSection);

        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(appDataPath, dbSettings.Name + ".sqlite");

        services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

        return services;
    }
}

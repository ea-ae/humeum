using Application.Common.Interfaces;

using Infrastructure.Common;
using Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Infrastructure.Identity;

using Microsoft.Extensions.Identity;
using Microsoft.AspNetCore.Identity;

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

        services.AddAuthentication();

        services.AddIdentity<ApplicationUser, IdentityRole<int>>(o => {
            o.Stores.ProtectPersonalData = true;
            o.Stores.MaxLengthForKeys = 128;

            o.Password.RequiredLength = 12;
            o.Password.RequiredUniqueChars = 3;
            o.Password.RequireDigit = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireLowercase = false;

            o.User.RequireUniqueEmail = true;
            o.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        return services;
    }
}

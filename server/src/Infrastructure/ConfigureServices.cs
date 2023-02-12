using Application.Common.Interfaces;
using Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infrastructure.Common.Settings;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
    const string _allowedUsernameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
                                                                     IConfiguration config) {
        var dbSettingsSection = config.GetSection(nameof(DatabaseSettings));
        var dbSettings = dbSettingsSection.Get<DatabaseSettings>()!;
        services.Configure<DatabaseSettings>(dbSettingsSection);

        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(appDataPath, dbSettings.Name + ".sqlite");
        services.AddDbContext<IAppDbContext, ApplicationDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

        var jwtSettingsSection = config.GetSection(nameof(JwtSettings));
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>()!;
        services.Configure<JwtSettings>(jwtSettingsSection);

        services
            .AddAuthentication(o => {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //.AddCookie(o => {
            //    o.ExpireTimeSpan = TimeSpan.FromMinutes(180);
            //    o.SlidingExpiration = true;
            //})
            .AddJwtBearer(o => {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new() {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.Zero
                };
                //o.Events.OnMessageReceived = async (context) => {
                //    if (context.Request.Cookies.TryGetValue("")) {

                //    }
                //}
            });

        services
            .AddIdentity<ApplicationUser, IdentityRole<int>>(o => {
                //o.Stores.ProtectPersonalData = true;
                o.Stores.MaxLengthForKeys = 128;

                o.Password.RequiredLength = 12;
                o.Password.RequiredUniqueChars = 3;
                o.Password.RequireDigit = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireLowercase = false;

                o.User.RequireUniqueEmail = true;
                o.User.AllowedUserNameCharacters = _allowedUsernameCharacters;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IApplicationUserService, JwtApplicationUserService>();

        return services;
    }
}

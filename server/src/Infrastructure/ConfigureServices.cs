using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Application.Common.Interfaces;

using Infrastructure.Common.Settings;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Configures infrastructure services.
/// </summary>
public static class ConfigureServices {
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

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        services
            .AddAuthentication()
            //.AddCookie(o => { /*o.ExpireTimeSpan = TimeSpan.FromMinutes(180); */o.SlidingExpiration = true; })
            .AddJwtBearer(o => {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new() {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),

                    ClockSkew = TimeSpan.Zero,
                };
                o.Events = new JwtBearerEvents {
                    OnMessageReceived = (context) => {
                        if (context.Request.Cookies.TryGetValue(jwtSettings.Cookie, out string? cookie)) {
                            context.Token = cookie;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization(o => {
            o.AddPolicy("CanHandleUserData", builder => builder.AddRequirements(new UserDataAccessRequirement()));
        });

        services.AddSingleton<IAuthorizationHandler, UserIdMatchHandler>();

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
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IApplicationUserService, JwtApplicationUserService>();

        return services;
    }
}

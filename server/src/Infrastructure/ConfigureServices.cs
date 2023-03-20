using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Application.Common.Interfaces;

using Infrastructure.Common.Settings;
using Infrastructure.Auth;
using Infrastructure.Persistence;
using Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

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

        if (dbSettings.Database.ToLower() == "sqlite") {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dbPath = Path.Combine(appDataPath, "humeum", dbSettings.Name + ".sqlite");
            string connectionString = $"Data Source={dbPath}";
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlite());
        } else if (dbSettings.Database.ToLower() == "postgres") {
            string connectionString = 
                $"Host={dbSettings.Host}; Pooling=true; Database={dbSettings.Name}; Port=5432;" +
                $"Username={dbSettings.Username}; Password={dbSettings.Password}";
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseNpgsql(connectionString));
        } else {
            throw new InvalidOperationException($"Database configuration type '${dbSettings.Database}' is invalid; use 'sqlite' or 'postgres'.");
        }

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
            o.AddPolicy("CanHandleUserData", builder => builder.AddRequirements(new UserOwnershipRequirement()));
            o.AddPolicy("CanHandleProfileData", builder => builder.AddRequirements(new UserOwnershipRequirement(),
                                                                                   new ProfileOwnershipRequirement()));
        });

        services.AddSingleton<IAuthorizationHandler, UserIdHandler>();
        services.AddSingleton<IAuthorizationHandler, ProfileIdHandler>();

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
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IApplicationUserService, JwtApplicationUserService>();

        return services;
    }

    /// <summary>
    /// Automatically apply any pending database migrations. Useful in production.
    /// </summary>
    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app) {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        
        if (context.Database.GetPendingMigrations().Any()) {
            context.Database.Migrate();
        }

        return app;
    }
}

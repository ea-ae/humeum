﻿using System.Data.Common;

using Application.Common.Interfaces;

using Infrastructure.Persistence;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Test.Common;

public class CustomWebAppFactory : WebApplicationFactory<Program> {
    HttpClient? _configuredClient;
    /// <summary>
    /// Correctly pre-configured HttpClient with an API root URL to be used in tests.
    /// </summary>
    public HttpClient ConfiguredClient {
        get {
            if (_configuredClient is null) {
                _configuredClient ??= CreateClient(new WebApplicationFactoryClientOptions {
                    BaseAddress = new Uri("http://localhost/api/v1/"),
                    AllowAutoRedirect = false,
                    HandleCookies = false
                });
                _configuredClient.DefaultRequestHeaders.Add("X-Requested-With", "HttpClient");
            }
            return _configuredClient;
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureTestServices(services => {

            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (dbContextDescriptor is not null) {
                services.Remove(dbContextDescriptor);
            }

            var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));
            if (dbConnectionDescriptor is not null) {
                services.Remove(dbConnectionDescriptor);
            }

            services.AddSingleton<DbConnection>(_ => {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open(); // prevent EF from automatically closing connection
                return connection;
            });

            services.AddDbContext<IAppDbContext, AppDbContext>((container, options) => {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });

            builder.UseEnvironment("Development");
        });
    }
}

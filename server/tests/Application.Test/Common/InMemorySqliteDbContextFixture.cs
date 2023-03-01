using Application.Common.Interfaces;

using Infrastructure.Persistence;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Application.Test.Common;

public class InMemorySqliteDbContextFixture : IDisposable {
    readonly SqliteConnection _connection;

    public InMemorySqliteDbContextFixture() {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open(); // prevent EF from automatically closing the connection
    }

    /// <summary>
    /// Sets up the database if required and returns a new DbContext instance.
    /// </summary>
    /// <returns>New DbContext instance ready for use.</returns>
    public IAppDbContext CreateDbContext() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(_connection).Options;
        IAppDbContext context = new ApplicationDbContext(options);
        // context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return context;
    }

    public void Dispose() {
        _connection.Dispose();
    }
}

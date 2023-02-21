using Domain.AssetAggregate;
using Domain.ProfileAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces;

public interface IAppDbContext {
    DbSet<Profile> Profiles { get; set; }
    DbSet<Transaction> Transactions { get; set; }
    DbSet<TransactionType> TransactionTypes { get; set; }
    DbSet<TimeUnit> TransactionTimeUnits { get; set; }
    DbSet<Asset> Assets { get; set; }

    public DatabaseFacade Database { get; }

    public int SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}

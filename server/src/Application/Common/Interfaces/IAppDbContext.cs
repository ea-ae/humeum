using Domain.AssetAggregate;
using Domain.ProfileAggregate;
using Domain.TaxSchemeAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;
using Domain.TransactionCategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces;

public interface IAppDbContext {
    DbSet<Profile> Profiles { get; set; }
    DbSet<Transaction> Transactions { get; set; }
    DbSet<TransactionType> TransactionTypes { get; set; }
    DbSet<TimeUnit> TransactionTimeUnits { get; set; }
    DbSet<TransactionCategory> TransactionCategories { get; set; }
    DbSet<Asset> Assets { get; set; }
    DbSet<AssetType> AssetTypes { get; set; }
    DbSet<TaxScheme> TaxSchemes { get; set; }

    public DatabaseFacade Database { get; }

    public int SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}

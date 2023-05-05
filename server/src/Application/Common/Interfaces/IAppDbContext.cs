using Domain.V1.AssetAggregate;
using Domain.V1.AssetAggregate.ValueObjects;
using Domain.V1.ProfileAggregate;
using Domain.V1.TaxSchemeAggregate;
using Domain.V1.TransactionAggregate;
using Domain.V1.TransactionAggregate.ValueObjects;
using Domain.V1.TransactionCategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    public EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

    public int SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    /// <summary>Save changes without soft deletion.</summary>
    public Task<int> SaveChangesWithHardDeletionAsync(CancellationToken cancellationToken = default);
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}

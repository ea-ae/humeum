using Domain.Entities;
using Domain.ValueObjects;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IAppDbContext {
    DbSet<Transaction> Transactions { get; set; }
    DbSet<TransactionType> TransactionTypes { get; set; }
    DbSet<TimeUnit> TransactionTimeUnits { get; set; }

    public int SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}

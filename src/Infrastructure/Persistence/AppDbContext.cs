using Application.Common.Interfaces;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext {
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<TimeUnit> TransactionTimescales { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Transaction>().OwnsOne(t => t.Frequency);

        builder.Entity<TransactionType>().HasIndex(t => t.Code).IsUnique();
        builder.Entity<TransactionType>().HasData(TransactionType.Income.WithId<TransactionType>(1),
                                                  TransactionType.Expense.WithId<TransactionType>(2));

        builder.Entity<TimeUnit>().HasIndex(t => t.Code).IsUnique(); // todo: inheritance on Code?
        builder.Entity<TimeUnit>().Ignore(t => t.InTimeSpan);
        builder.Entity<TimeUnit>().HasData(TimeUnit.Hours.WithId<TimeUnit>(1), 
                                           TimeUnit.Days.WithId<TimeUnit>(2), 
                                           TimeUnit.Weeks.WithId<TimeUnit>(3), 
                                           TimeUnit.Months.WithId<TimeUnit>(4),
                                           TimeUnit.Years.WithId<TimeUnit>(5));
    }
}

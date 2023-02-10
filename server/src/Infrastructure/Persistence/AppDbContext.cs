using Application.Common.Interfaces;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext {
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<TimeUnit> TransactionTimeUnits { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        //builder.Entity<Transaction>().OwnsOne(t => t.Frequency);
        //builder.Entity<Transaction>().OwnsOne(t => t.PaymentPeriod);
        builder.Entity<Transaction>().OwnsOne(t => t.PaymentTimeline, pt => {
            pt.OwnsOne(pt => pt.Period);
            pt.OwnsOne(pt => pt.Frequency);
        });

        builder.Entity<TransactionType>().HasIndex(tt => tt.Code).IsUnique();
        builder.Entity<TransactionType>().HasData(TransactionType.Income, TransactionType.Expense);

        builder.Entity<TimeUnit>().HasIndex(tu => tu.Code).IsUnique();
        builder.Entity<TimeUnit>().Ignore(tu => tu.InTimeSpan);
        builder.Entity<TimeUnit>().HasData(TimeUnit.Days, TimeUnit.Weeks, TimeUnit.Months, TimeUnit.Years);
    }
}

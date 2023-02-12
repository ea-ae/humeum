using Application.Common.Interfaces;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;
using Domain.UserAggregate;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>, IAppDbContext {
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<TimeUnit> TransactionTimeUnits { get; set; }

    public new DbSet<User> Users { get; set; }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder); // configure Identity schema

        builder.Entity<Transaction>().OwnsOne(t => t.PaymentTimeline, pt => {
            pt.OwnsOne(pt => pt.Period);
            pt.OwnsOne(pt => pt.Frequency);
        });

        builder.Entity<TransactionType>().HasIndex(tt => tt.Code).IsUnique();
        builder.Entity<TransactionType>().HasData(TransactionType.Income, TransactionType.Expense);

        builder.Entity<TimeUnit>().HasIndex(tu => tu.Code).IsUnique();
        builder.Entity<TimeUnit>().Ignore(tu => tu.InTimeSpan);
        builder.Entity<TimeUnit>().HasData(TimeUnit.Days, TimeUnit.Weeks, TimeUnit.Months, TimeUnit.Years);

        builder.Entity<User>().OwnsOne(u => u.Username);
    }
}

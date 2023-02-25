using Application.Common.Interfaces;

using Domain.AssetAggregate;
using Domain.Common;
using Domain.ProfileAggregate;
using Domain.TaxSchemeAggregate;
using Domain.TaxSchemeAggregate.ValueObjects;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>, IAppDbContext {
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<TimeUnit> TransactionTimeUnits { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<TaxScheme> TaxSchemes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        SavingChanges += SetTimestampFields; // add event handler
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder); // configure Identity schema

        builder.Entity<Transaction>().HasOne(t => t.Profile).WithMany(p => p.Transactions);
        builder.Entity<Transaction>().HasOne(t => t.TaxScheme).WithMany(ts => ts.Transactions);
        builder.Entity<Transaction>().HasOne(t => t.Asset).WithMany(a => a.Transactions);
        builder.Entity<Transaction>().OwnsOne(t => t.PaymentTimeline, pt => {
            pt.OwnsOne(pt => pt.Period);
            pt.OwnsOne(pt => pt.Frequency, f => {
                f.HasOne(f => f.TimeUnit);
            });
        });

        builder.Entity<TransactionType>().HasIndex(tt => tt.Code).IsUnique();
        builder.Entity<TransactionType>().HasData(TransactionType.Always,
                                                  TransactionType.PreRetirementOnly,
                                                  TransactionType.RetirementOnly);

        builder.Entity<TimeUnit>().HasIndex(tu => tu.Code).IsUnique();
        builder.Entity<TimeUnit>().Ignore(tu => tu.InTimeSpan);
        builder.Entity<TimeUnit>().HasData(TimeUnit.Days, TimeUnit.Weeks, TimeUnit.Months, TimeUnit.Years);

        builder.Entity<Asset>().HasOne(a => a.Profile).WithMany(p => p.Assets).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Asset>().HasData(
            new Asset(1,
                "Index fund (default)",
                "Index funds track the performance of a particular market index; great diversification, low fees, and easy management.",
                returnRate: 8.1m,
                standardDeviation: 15.2m),
            new Asset(2,
                "Bond fund (default)",
                "Bond funds provide great diversification potential and are stereotypically less volatile than other securities.",
                returnRate: 1.9m,
                standardDeviation: 3.0m)
        );

        builder.Entity<TaxScheme>().HasData(
            new TaxScheme(1,
                "Income tax",
                "Regular flat income tax in Estonia, applicable to all income by default. First 654EUR/mo aka 7848EUR/yr are tax-free.",
                20,
                new TaxIncentiveScheme(20, null, null, 7848)),
            new TaxScheme(2,
                "III pillar, post-2021",
                "Asset income invested through III pillar, with an account opened in 2021 or later. " +
                "Term pensions based on life expectancy, not included here, provide a 20% discount.",
                20,
                new TaxIncentiveScheme(10, 60, 15, 6000)),
            new TaxScheme(3,
                "III pillar, pre-2021",
                "Asset income invested through III pillar, with an account opened before 2021. " +
                "Term pensions based on life expectancy, not included here, provide a 20% discount.",
                20,
                new TaxIncentiveScheme(10, 55, 15, 6000)),
            new TaxScheme(4,
                "Non-taxable income",
                "Income that due to special circumstances (e.g. charity) is not taxed whatsoever.",
                0)
        );
        builder.Entity<TaxScheme>().OwnsOne(ts => ts.IncentiveScheme).HasData( // required workaround for EF core owned entity seeding
            new TaxIncentiveScheme(20, null, null, 7848) { TaxSchemeId = 1 });
    }

    public override int SaveChanges() {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Source: https://stackoverflow.com/a/74052251/4362799.
    /// </summary>
    private void SetTimestampFields(object? sender, SavingChangesEventArgs eventArgs) {
        var context = (DbContext)(sender ?? throw new InvalidOperationException());

        var entities = context.ChangeTracker.Entries().Where(e => e.State is EntityState.Modified or EntityState.Deleted);

        foreach (var entity in entities) {
            if (entity.Entity is TimestampedEntity timestampedEntity) {
                if (entity.State is EntityState.Modified) {
                    timestampedEntity.UpdateModificationTimestamp();
                } else if (entity.State is EntityState.Deleted) {
                    timestampedEntity.SetDeletionTimestamp();
                    entity.State = EntityState.Modified;
                }
            } else if (entity.Entity is ValueObject valueObject) {
                if (entity.State is EntityState.Deleted) {
                    entity.State = EntityState.Modified;
                }
            }
        }
    }
}

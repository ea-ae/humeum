using Application.Common.Interfaces;

using Domain.AssetAggregate;
using Domain.Common;
using Domain.ProfileAggregate;
using Domain.TaxSchemeAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;
using Domain.TransactionCategoryAggregate;

using Infrastructure.Auth;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>, IAppDbContext {
    public DbSet<Profile> Profiles { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<TransactionType> TransactionTypes { get; set; } = null!;
    public DbSet<TimeUnit> TransactionTimeUnits { get; set; } = null!;
    public DbSet<TransactionCategory> TransactionCategories { get; set; } = null!;
    public DbSet<Asset> Assets { get; set; } = null!;
    public DbSet<AssetType> AssetTypes { get; set; } = null!;
    public DbSet<TaxScheme> TaxSchemes { get; set; } = null!;

    /// <summary>Boolean switch for enabling or disabling soft deletion in a given DbContext.</summary>
    bool _softDeletionMode = true;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        SavingChanges += SetTimestampFields; // add event handler
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder); // configure Identity schema

        builder.Entity<Transaction>().HasOne(t => t.Type);
        builder.Entity<Transaction>().HasOne(t => t.Profile).WithMany(p => p.Transactions);
        builder.Entity<Transaction>().HasOne(t => t.TaxScheme).WithMany(ts => ts.Transactions);
        builder.Entity<Transaction>().HasOne(t => t.Asset).WithMany(a => a.Transactions);
        builder.Entity<Transaction>().HasMany(t => t.Categories).WithMany(tc => tc.Transactions)
                                     .UsingEntity(j => j.ToTable("TransactionWithCategory"));
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

        builder.Entity<TransactionCategory>().HasOne(tc => tc.Profile).WithMany(p => p.TransactionCategories);
        builder.Entity<TransactionCategory>().HasData(
            new TransactionCategory(1, "General"),
            new TransactionCategory(2, "Investing"),
            new TransactionCategory(3, "Work, Education, & Business"),
            new TransactionCategory(4, "Recreation & Lifestyle"),
            new TransactionCategory(5, "Food & Clothing"),
            new TransactionCategory(6, "Housing & Utilities"),
            new TransactionCategory(7, "Transportation"),
            new TransactionCategory(8, "Gifts & Donations")
        );

        builder.Entity<Asset>().HasOne(a => a.Type);
        builder.Entity<Asset>().HasOne(a => a.Profile).WithMany(p => p.Assets).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Asset>().HasData(
            new Asset(1,
                "Index fund (default)",
                "Index funds track the performance of a particular market index; great diversification, low fees, and easy management.",
                returnRate: 8.1m,
                standardDeviation: 15.2m,
                typeId: AssetType.Index.Id),
            new Asset(2,
                "Bond fund (default)",
                "Bond funds provide great diversification potential and are stereotypically less volatile than other securities.",
                returnRate: 1.9m,
                standardDeviation: 3.0m,
                typeId: AssetType.Bond.Id)
        );

        builder.Entity<AssetType>().HasData(AssetType.Liquid,
                                            AssetType.Index,
                                            AssetType.Managed,
                                            AssetType.RealEstate,
                                            AssetType.Bond,
                                            AssetType.Stock,
                                            AssetType.Other);

        builder.Entity<TaxScheme>().HasData(
            new TaxScheme(1,
                "Income tax",
                "Regular flat income tax in Estonia, applicable to all income by default. First 654EUR/mo aka 7848EUR/yr are tax-free.",
                20),
            new TaxScheme(2,
                "III pillar, post-2021",
                "Asset income invested through III pillar, with an account opened in 2021 or later. " +
                "Term pensions based on life expectancy, not included here, provide a 20% discount.",
                20),
            new TaxScheme(3,
                "III pillar, pre-2021",
                "Asset income invested through III pillar, with an account opened before 2021. " +
                "Term pensions based on life expectancy, not included here, provide a 20% discount.",
                20),
            new TaxScheme(4,
                "Non-taxable income",
                "Income that due to special circumstances (e.g. charity) is not taxed whatsoever.",
                0)
        );
        builder.Entity<TaxScheme>().OwnsOne(ts => ts.IncentiveScheme).HasData( // required workaround for EF core owned entity seeding bug
            new { TaxSchemeId = 1, TaxRefundRate = 20m, MaxApplicableIncome = 7848 },
            new { TaxSchemeId = 2, TaxRefundRate = 10m, MinAge = 60, MaxIncomePercentage = 15m, MaxApplicableIncome = 6000 },
            new { TaxSchemeId = 3, TaxRefundRate = 10m, MinAge = 55, MaxIncomePercentage = 15m, MaxApplicableIncome = 6000 }
        );
    }

    public async Task<int> SaveChangesWithHardDeletionAsync(CancellationToken cancellationToken = default) {
        _softDeletionMode = false;
        int result = await SaveChangesAsync(cancellationToken);
        _softDeletionMode = true;
        return result;
    }

    public override int SaveChanges() {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Source: https://stackoverflow.com/a/74052251/4362799.
    /// </summary>
    private void SetTimestampFields(object? sender, SavingChangesEventArgs eventArgs) {
        var context = (AppDbContext)(sender ?? throw new InvalidOperationException());

        if (!context._softDeletionMode) {
            return; // soft deletion is disabled
        }

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

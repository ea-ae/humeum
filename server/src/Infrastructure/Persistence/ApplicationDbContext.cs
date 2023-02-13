﻿using Application.Common.Interfaces;

using AutoMapper.Execution;

using Domain.Common;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;
using Domain.UserAggregate;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>, IAppDbContext {
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<TimeUnit> TransactionTimeUnits { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder); // configure Identity schema

        //builder.Entity<User>().Property<ApplicationUser>("ApplicationUser");
        //builder.Entity<User>().Property<int>("ApplicationUserId");
        //builder.Entity<User>().HasOne("ApplicationUser").WithOne().HasForeignKey("ApplicationUserId");

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

    public override int SaveChanges() {
        throw new NotSupportedException();
    }

    public async Task<int> SaveChangesAsync() {
        ChangeTracker.DetectChanges();

        var entities = ChangeTracker.Entries().Where(e => e.State is EntityState.Modified or EntityState.Deleted);

        foreach (var entity in entities) {
            if (entity.Entity is TimestampedEntity timestampedEntity) {
                if (entity.State is EntityState.Modified) {
                    timestampedEntity.UpdateModificationTimestamp();
                } else if (entity.State is EntityState.Deleted) {
                    timestampedEntity.UpdateDeletionTimestamp();
                    entity.State = EntityState.Modified;
                }
            }
        }

        return await base.SaveChangesAsync();
    }
}

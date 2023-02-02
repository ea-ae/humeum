﻿using Application.Common.Interfaces;

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
        builder.Entity<TransactionType>().HasData(TransactionType.Income, TransactionType.Expense);

        builder.Entity<TimeUnit>().HasIndex(t => t.Code).IsUnique(); // todo: inheritance on Code?
        builder.Entity<TimeUnit>().Ignore(t => t.InTimeSpan);
        builder.Entity<TimeUnit>().HasData(TimeUnit.Hours, TimeUnit.Days, TimeUnit.Weeks, 
                                           TimeUnit.Months, TimeUnit.Years);
    }
}

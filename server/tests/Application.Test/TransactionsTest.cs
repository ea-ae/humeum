using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Test.Common;
using Application.Transactions.Commands;
using Application.Transactions.Queries;

using AutoMapper;

using Domain.TaxSchemeAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

using Xunit;

namespace Application.Test;

/// <summary>
/// Tests centered generally around transaction services.
/// </summary>
[Collection(InMemoryDbContextCollection.COLLECTION_NAME)]
public class TransactionsTest {
    readonly InMemorySqliteDbContextFixture _dbContextFixture;

    public TransactionsTest(InMemorySqliteDbContextFixture fixture) {
        _dbContextFixture = fixture;
    }

    [Fact]
    public async Task GetTransactionsQuery_TransactionsAndProfiles_ReturnsAuthenticatedProfileTransactions() {
        const int taxSchemeId = 1;
        var context = _dbContextFixture.CreateDbContext();
        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new AppMappingProfile()); });
        var handler = new GetTransactionsQueryHandler(context, mapperConfig.CreateMapper());

        // set up a profile and three transactions

        var profile = new Domain.ProfileAggregate.Profile(100, "Default");
        context.Profiles.Add(profile);
        await context.SaveChangesAsync();

        var transactionType = TransactionType.Always;
        context.TransactionTypes.Attach(transactionType);

        var timeline = new Timeline(new TimePeriod(new DateOnly(2023, 2, 3)));
        var transaction = new Transaction("Test", null, 42, transactionType, timeline, profile.Id, taxSchemeId);
        context.Transactions.Add(transaction);

        var timeUnit = TimeUnit.Years;
        context.TransactionTimeUnits.Attach(timeUnit);
        timeline = new Timeline(new TimePeriod(new DateOnly(2022, 6, 6), new DateOnly(2024, 1, 1)), new Frequency(timeUnit, 2, 3));

        transaction = new Transaction("Test2", null, 43, transactionType, timeline, profile.Id, taxSchemeId);
        context.Transactions.Add(transaction);

        timeline = new Timeline(new TimePeriod(new DateOnly(2013, 1, 1)));
        transaction = new Transaction("Test2", null, 43, transactionType, timeline, profile.Id, taxSchemeId);
        context.Transactions.Add(transaction);

        await context.SaveChangesAsync();

        // ensure that endpoint returns transactions

        GetTransactionsQuery query = new() { Profile = profile.Id };
        var result = await handler.Handle(query);
        Assert.Equal(3, result.Count);

        // ensure that endpoint returns transactions with filters

        query = new() { Profile = profile.Id, StartAfter = new DateOnly(2013, 1, 2) };
        result = await handler.Handle(query);
        Assert.Equal(2, result.Count);

        query = new() { Profile = profile.Id, StartBefore = new DateOnly(2023, 12, 1), StartAfter = new DateOnly(2022, 6, 7) };
        result = await handler.Handle(query);
        Assert.Single(result);

        // ensure that a new profile with no transactions returns an empty list

        var emptyProfile = new Domain.ProfileAggregate.Profile(200, "EmptyProfile");
        context.Profiles.Add(emptyProfile);
        await context.SaveChangesAsync();

        query = new() { Profile = emptyProfile.Id };
        result = await handler.Handle(query);
        Assert.Empty(result);

        // ensure that profiles owned by different users do not leak data

        query = new() { Profile = emptyProfile.Id };
        _ = Assert.ThrowsAsync<NotFoundValidationException>(async () => await handler.Handle(query));

        query = new() { Profile = profile.Id };
        _ = Assert.ThrowsAsync<NotFoundValidationException>(async () => await handler.Handle(query));
    }

    [Fact]
    public async Task AddTransactionCommand_OneTimeTransaction_Persists() {
        var context = _dbContextFixture.CreateDbContext();
        var handler = new AddTransactionCommandHandler(context);

        var profile = new Domain.ProfileAggregate.Profile(1, "Default");
        context.Profiles.Add(profile);
        await context.SaveChangesAsync();

        AddTransactionCommand command = new() {
            Profile = profile.Id,
            Amount = -200,
            Name = "Groceries",
            Description = "My groceries",
            Type = "ALWAYS",
            PaymentStart = new DateOnly(2022, 3, 1),
            TaxScheme = 1
        };

        // create transaction

        var result = await handler.Handle(command);
        var transaction = context.Transactions.First(t => t.Id == result);

        // confirm details

        Assert.Equal(profile.Id, transaction.ProfileId);
        Assert.Equal(command.Amount, transaction.Amount);
        Assert.Equal(command.Name, transaction.Name);
        Assert.Equal(command.Description, transaction.Description);
        Assert.Equal(command.Type, transaction.Type.Code);
        Assert.Equal(command.PaymentStart, transaction.PaymentTimeline.Period.Start);
        Assert.Null(transaction.PaymentTimeline.Frequency);
        Assert.Null(transaction.PaymentTimeline.Period.End);
        Assert.Null(transaction.DeletedAt);
    }

    [Fact]
    public async Task AddTransactionCommand_RecurringTransaction_Persists() {
        var context = _dbContextFixture.CreateDbContext();
        var handler = new AddTransactionCommandHandler(context);

        var profile = new Domain.ProfileAggregate.Profile(1, "Default");
        context.Profiles.Add(profile);
        await context.SaveChangesAsync();

        // create transaction

        AddTransactionCommand command = new() {
            Profile = profile.Id,
            Amount = 150,
            Name = "Shoplifting",
            Description = "My shoplifted groceries",
            Type = "PRERETIREMENTONLY",
            PaymentStart = new DateOnly(2016, 3, 1),
            PaymentEnd = new DateOnly(2021, 1, 1),
            TimeUnit = "WEEKS",
            TimesPerCycle = 3,
            UnitsInCycle = 2,
            TaxScheme = 1,
        };

        var result = await handler.Handle(command);
        var transaction = context.Transactions.First(t => t.Id == result);

        // confirm details

        Assert.Equal(profile.Id, transaction.ProfileId);
        Assert.Equal(command.Amount, transaction.Amount);
        Assert.Equal(command.Name, transaction.Name);
        Assert.Equal(command.Description, transaction.Description);
        Assert.Equal(command.Type, transaction.Type.Code);
        Assert.Equal(command.PaymentStart, transaction.PaymentTimeline.Period.Start);
        Assert.Equal(command.PaymentEnd, transaction.PaymentTimeline.Period.End);
        Assert.Equal(command.TimeUnit, transaction.PaymentTimeline.Frequency?.TimeUnit.Code);
        Assert.Equal(command.TimesPerCycle, transaction.PaymentTimeline.Frequency?.TimesPerCycle);
        Assert.Equal(command.UnitsInCycle, transaction.PaymentTimeline.Frequency?.UnitsInCycle);
        Assert.Null(transaction.DeletedAt);
    }

    [Fact]
    public async Task ReplaceTransactionCommand_OneTimeTransaction_ReplacesWithRecurringTransaction() {
        var context = _dbContextFixture.CreateDbContext();
        var handler = new ReplaceTransactionCommandHandler(context);

        var profile = new Domain.ProfileAggregate.Profile(1, "Default");
        context.Profiles.Add(profile);
        await context.SaveChangesAsync();

        // create data

        var transactionType = TransactionType.Always;
        context.TransactionTypes.Attach(transactionType);
        var taxScheme = context.TaxSchemes.First(t => t.Id == 1);
        var transaction = new Transaction("Hello", null, 10, transactionType, new Timeline(new TimePeriod(new DateOnly(2022, 1, 1))), profile, taxScheme);
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        // replace data

        ReplaceTransactionCommand command = new() {
            Transaction = transaction.Id,
            Profile = profile.Id,
            Amount = -10,
            Name = null,
            Description = null,
            Type = "RETIREMENTONLY",
            PaymentStart = new DateOnly(2016, 1, 1),
            PaymentEnd = new DateOnly(2021, 1, 1),
            TimeUnit = "WEEKS",
            TimesPerCycle = 1,
            UnitsInCycle = 1,
            TaxScheme = 2,
            Asset = 1
        };

        var result = await handler.Handle(command);
        await context.SaveChangesAsync();
        await context.Entry(transaction).ReloadAsync(); // reload data

        // assert that changed fields match

        Assert.Equal(command.Amount, transaction.Amount);
        Assert.Equal(command.Name, transaction.Name);
        Assert.Equal(TransactionType.RetirementOnly.Id, transaction.TypeId);
        Assert.Equal(command.PaymentStart, transaction.PaymentTimeline.Period.Start);
        Assert.Equal(command.PaymentEnd, transaction.PaymentTimeline.Period.End);
        Assert.Equal(TimeUnit.Weeks.Id, transaction.PaymentTimeline.Frequency?.TimeUnitId);
        Assert.Equal(command.TimesPerCycle, transaction.PaymentTimeline.Frequency?.TimesPerCycle);
        Assert.Equal(command.UnitsInCycle, transaction.PaymentTimeline.Frequency?.UnitsInCycle);
        Assert.Equal(command.TaxScheme, transaction.TaxSchemeId);
        Assert.Equal(command.Asset, transaction.AssetId);
    }
}

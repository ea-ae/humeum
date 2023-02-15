using Xunit;

using Application.Test.Common;
using Application.Transactions.Commands.AddTransaction;
using Domain.ProfileAggregate;

namespace Application.Test.Transactions;

[Collection(InMemoryDbContextCollection.COLLECTION_NAME)]
public class TransactionCommandTests {
    readonly InMemorySqliteDbContextFixture _dbContextFixture;

    public TransactionCommandTests(InMemorySqliteDbContextFixture fixture) {
        _dbContextFixture = fixture;
    }

    [Fact]
    public async void AddTransactionCommand_OneTimeTransaction_Persists() {
        var context = _dbContextFixture.CreateDbContext();
        var handler = new AddTransactionCommandHandler(context);
        
        var profile = new Profile(1, "Default");
        context.Profiles.Add(profile);
        await context.SaveChangesAsync();

        AddTransactionCommand command = new() {
            Profile = profile.Id,
            Amount = 200,
            Name = "Groceries",
            Description = "My groceries",
            Type = "ALWAYS",
            PaymentStart = new DateOnly(2022, 3, 1)
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
}

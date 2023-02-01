using Domain;

using Xunit;

namespace Test.Domain;

public class TransactionTest {
    [Fact]
    public void TotalTransactionCount_InstantEnd_ReturnsOne() {
        var transaction = new Transaction {
            Type = new TransactionType {
                Name = "Income",
                Code = "INCOME",
            },
            Timescale = new TransactionTimescale {
                Name = "Days",
                Code = "DAYS",
            },
            PerTimescale = false,
            Start = new DateTime(2045, 5, 4, 12, 50, 40),
            End = new DateTime(2045, 5, 4, 12, 50, 40),
        };

        int expected = 1;

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }
}

using Domain.Entities;

using Xunit;

namespace Test.Domain;

public class TransactionTest {
    [Fact]
    public void TotalTransactionCount_InstantEnd_ReturnsOne() {
        var transaction = BuildTransaction("DAYS", new DateTime(2045, 5, 4, 12, 50, 40), null);

        int expected = 1;

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TotalTransactionCount_Days_CountsCorrectly() {
        var transaction = BuildTransaction("days", new DateTime(2045, 5, 4, 12, 50, 40), new DateTime(2045, 5, 15, 17, 52, 42));

        int expected = (15 - 4) + 1; // 12

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TotalTransactionCount_CompleteWeeks_CountsCorrectly() {
        var transaction = BuildTransaction("weeks", new DateTime(2045, 5, 1), new DateTime(2046, 5, 15));

        int expected = 52 + 2 + 1; // 55

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// 1st of January is a Sunday and 14th of January is a Saturday, which does not count as a full week.
    /// </summary>
    [Fact]
    public void TotalTransactionCount_IncompleteWeeks_CountsCorrectly() {
        var transaction = BuildTransaction("weeks", new DateTime(2023, 1, 1), new DateTime(2023, 1, 14));

        int expected = 1 + 1; // 2

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// 1st of January 2023 is a Sunday and 8th of January 2024 is a Saturday.
    /// </summary>
    [Fact]
    public void TotalTransactionCount_IncompleteWeeksOverYears_CountsCorrectly() {
        var transaction = BuildTransaction("weeks", new DateTime(2023, 1, 1), new DateTime(2024, 1, 8));

        // 53 sundays in 2023, 1 sunday on 7th January
        int expected = 53 + 1; // 2

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TotalTransactionCount_CompleteMonths_CountsCorrectly() {
        var transaction = BuildTransaction("months", new DateTime(2023, 1, 1), new DateTime(2023, 3, 2));

        int expected = 2 + 1; // 3

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// We start on the 2nd, but end on the 1st; we only count January and February.
    /// </summary>
    [Fact]
    public void TotalTransactionCount_IncompleteMonths_CountsCorrectly() {
        var transaction = BuildTransaction("months", new DateTime(2023, 1, 2), new DateTime(2023, 3, 1));

        int expected = 1 + 1; // 2

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TotalTransactionCount_IncompleteMonthsOverYears_CountsCorrectly() {
        var transaction = BuildTransaction("months", new DateTime(2023, 1, 2), new DateTime(2025, 2, 1));

        int expected = 2 * 12 + 1; // 25

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TotalTransactionCount_IncompleteMonthsOverYearsWithLeapYear_CountsCorrectly() {
        var transaction = BuildTransaction("months", new DateTime(2020, 2, 29), new DateTime(2021, 2, 28));

        int expected = 12 + 1; // 13

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TotalTransactionCount_CompleteYears_CountsCorrectly() {
        var transaction = BuildTransaction("years", new DateTime(2023, 1, 1), new DateTime(2030, 1, 1));

        int expected = 7 + 1; // 8

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TotalTransactionCount_IncompleteYears_CountsCorrectly() {
        var transaction = BuildTransaction("years", new DateTime(2023, 1, 2), new DateTime(2030, 1, 1));

        int expected = 6 + 1; // 7

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TotalTransactionCount_IncompleteYearsWithLeapDay_CountsCorrectly() {
        var transaction = BuildTransaction("years", new DateTime(2020, 2, 29), new DateTime(2030, 2, 28));

        int expected = 10 + 1; // 11

        int actual = transaction.TotalTransactionCount;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Construct a partial Transaction object for testing purposes.
    /// </summary>
    /// <returns>Partially instantiated transaction.</returns>
    private Transaction BuildTransaction(string timescale, DateTime paymentStart, DateTime? paymentEnd) {
        var transaction = new Transaction {
            Type = new TransactionType {
                Name = "Income",
                Code = "INCOME",
            },
            Timescale = new TransactionTimescale {
                Name = timescale.ToLower(),
                Code = timescale.ToUpper(),
            },
            PerTimescale = false,
            PaymentStart = paymentStart,
            PaymentEnd = paymentEnd,
        };

        return transaction;
    }
}

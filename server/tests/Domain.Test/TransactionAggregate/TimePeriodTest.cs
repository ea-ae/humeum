using Domain.TransactionAggregate.ValueObjects;

using Xunit;

namespace Domain.Test.TransactionAggregate;

public class TimePeriodTest {
    [Fact]
    public void TimePeriodConstructor_InvalidStartDate_ThrowsDomainException() {
        Assert.Throws<ArgumentException>(() => new TimePeriod(new DateOnly(2021, 1, 1), new DateOnly(2020, 1, 1)));
    }
}

using Domain.Common.Exceptions;
using Domain.V1.TransactionAggregate.ValueObjects;
using Xunit;

namespace Domain.Test.TransactionAggregate;

public class TimePeriodTest {
    [Fact]
    public void TimePeriodConstructor_InvalidStartDate_ThrowsDomainException() {
        // act

        var result = TimePeriod.Create(new DateOnly(2021, 1, 1), new DateOnly(2020, 1, 1));

        // assert

        Assert.True(result.Failure);
        Assert.Equal(1, result.GetErrors().Count);
        Assert.IsType<DomainException>(result.GetErrors().First());
    }
}

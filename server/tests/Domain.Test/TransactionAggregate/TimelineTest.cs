using Domain.Common.Exceptions;
using Domain.V1.TransactionAggregate.ValueObjects;
using Xunit;

namespace Domain.Test.TransactionAggregate;

public class TimelineTest {
    [Fact]
    public void TimelineConstructor_FrequencyWithNoEndDate_ThrowsDomainException() {
        // arrange

        var timePeriod = TimePeriod.Create(new DateOnly(2022, 1, 1)).Unwrap();
        var frequency = Frequency.Create(TimeUnit.Weeks, 3, 2).Unwrap();

        // act

        var actual = Timeline.Create(timePeriod, frequency);

        // assert

        Assert.True(actual.Failure);
        Assert.Equal(1, actual.GetErrors().Count);
        Assert.IsType<DomainException>(actual.GetErrors().First());
    }

    [Fact]
    public void TimelineConstructor_NoFrequencyWithEndDate_ThrowsDomainException() {
        // arrange

        var period = TimePeriod.Create(new DateOnly(2022, 1, 1), new DateOnly(2023, 1, 1)).Unwrap();

        // act

        var actual = Timeline.Create(period);

        // assert

        Assert.True(actual.Failure);
        Assert.Equal(1, actual.GetErrors().Count);
        Assert.IsType<DomainException>(actual.GetErrors().First());
    }
}

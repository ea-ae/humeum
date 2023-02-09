using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

using Xunit;

namespace Domain.Test.TransactionAggregate;

public class TimelineTest {
    [Fact]
    public void TimelineConstructor_FrequencyWithNoEndDate_ThrowsDomainException() {
        var timePeriod = new TimePeriod(new DateTime(2022, 1, 1));
        var frequency = new Frequency(TimeUnit.Weeks, 3);

        Assert.Throws<ArgumentException>(() => new Timeline(timePeriod, frequency));
    }

    [Fact]
    public void TimelineConstructor_NoFrequencyWithEndDate_ThrowsDomainException() {
        var timePeriod = new TimePeriod(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1));

        Assert.Throws<ArgumentException>(() => new Timeline(timePeriod));
    }
}

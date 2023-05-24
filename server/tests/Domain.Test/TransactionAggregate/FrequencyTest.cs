using Domain.V1.TransactionAggregate.ValueObjects;

using Xunit;

namespace Domain.Test.TransactionAggregate;

public class FrequencyTest {
    [Fact]
    public void AddPeriodToTime_OneDay_AddsDay() {
        // arrange

        var frequency = Frequency.Create(TimeUnit.Days, 1, 1).Unwrap();
        var start = new DateTime(2000, 6, 19);
        var expected = new DateTime(2000, 6, 20);

        // act

        var actual = frequency.AddPeriodToTime(start);

        // assert

        Assert.Equal(expected, actual);
    }
}

using Domain.V1.TransactionAggregate.ValueObjects;

using Xunit;

namespace Domain.Test.TransactionAggregate;

public class FrequencyTest {
    [Fact]
    public void AddPeriodToTime_OnceEveryYear_AddsOneYear() {
        // arrange

        var frequency = Frequency.Create(TimeUnit.Years, 1, 1).Unwrap();
        var time = new DateTime(2000, 6, 19);
        var expected = new DateTime(2001, 6, 19);

        // act

        var actual = frequency.AddPeriodToTime(time);

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AddPeriodToTime_OnceEveryThreeDaysWithThreeCalls_AddsOneDay() {
        // arrange

        var frequency = Frequency.Create(TimeUnit.Days, 1, 3).Unwrap();
        var time = new DateTime(2000, 6, 19);
        var expected = new DateTime(2000, 6, 20);

        // act

        time = frequency.AddPeriodToTime(time);
        time = frequency.AddPeriodToTime(time);
        var actual = frequency.AddPeriodToTime(time);

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AddPeriodToTime_TwiceEveryMonth_AddsTwoDays() {
        // arrange

        var frequency = Frequency.Create(TimeUnit.Months, 2, 1).Unwrap();
        var time = new DateTime(2000, 6, 19);
        var expected = new DateTime(2000, 8, 19);

        // act

        var actual = frequency.AddPeriodToTime(time);

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AddPeriodToTime_TwiceEveryThreeDaysWithThreeCalls_AddsTwoDays() {
        // arrange

        var frequency = Frequency.Create(TimeUnit.Days, 2, 3).Unwrap();
        var time = new DateTime(2000, 6, 19);
        var expected = new DateTime(2000, 6, 21);

        // act

        time = frequency.AddPeriodToTime(time);
        time = frequency.AddPeriodToTime(time);
        var actual = frequency.AddPeriodToTime(time);

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AddPeriodToTime_TwiceEveryTwoYears_AddsOneYear() {
        // arrange

        var frequency = Frequency.Create(TimeUnit.Years, 2, 2).Unwrap();
        var time = new DateTime(2000, 6, 19);
        var expected = new DateTime(2001, 6, 19);

        // act

        var actual = frequency.AddPeriodToTime(time);

        // assert

        Assert.Equal(expected, actual);
    }
}

using System.ComponentModel.DataAnnotations;

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

    [Fact]
    public void GetPaymentDates_Once_ReturnsCorrectDate() {
        // arrange

        var period = TimePeriod.Create(new DateOnly(2000, 1, 1)).Unwrap();
        var timeline = Timeline.Create(period).Unwrap();

        var expected = new[] { new DateOnly(2000, 1, 1) };

        // act

        var actual = timeline.GetPaymentDates().Unwrap();

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPaymentDates_OnceADayForThreeDays_ReturnsCorrectDates() {
        // arrange

        var period = TimePeriod.Create(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 3)).Unwrap();
        var frequency = Frequency.Create(TimeUnit.Days, 1, 1).Unwrap();
        var timeline = Timeline.Create(period, frequency).Unwrap();
        
        var expected = new[] { new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 2), new DateOnly(2000, 1, 3) };

        // act

        var actual = timeline.GetPaymentDates().Unwrap();

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPaymentDates_TwiceADayForThreeDays_ReturnsCorrectDates() {
        // arrange

        var period = TimePeriod.Create(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 3)).Unwrap();
        var frequency = Frequency.Create(TimeUnit.Days, 2, 1).Unwrap();
        var timeline = Timeline.Create(period, frequency).Unwrap();

        var expected = new[] {
            new DateOnly(2000, 1, 1),
            new DateOnly(2000, 1, 1),
            new DateOnly(2000, 1, 2),
            new DateOnly(2000, 1, 2),
            new DateOnly(2000, 1, 3),
            new DateOnly(2000, 1, 3)
        };

        // act

        var actual = timeline.GetPaymentDates().Unwrap();

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPaymentDates_EverySecondDayForFiveDays_ReturnsCorrectDates() {
        // arrange

        var period = TimePeriod.Create(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 5)).Unwrap();
        var frequency = Frequency.Create(TimeUnit.Days, 1, 2).Unwrap();
        var timeline = Timeline.Create(period, frequency).Unwrap();

        var expected = new[] {
            new DateOnly(2000, 1, 2),
            new DateOnly(2000, 1, 4)
        };

        // act

        var actual = timeline.GetPaymentDates().Unwrap();

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPaymentDates_ThreeTimesAWeek_ReturnsCorrectDates() {
        // arrange

        var period = TimePeriod.Create(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 7)).Unwrap();
        var frequency = Frequency.Create(TimeUnit.Weeks, 3, 1).Unwrap();
        var timeline = Timeline.Create(period, frequency).Unwrap();

        var expected = new[] { new DateOnly(2000, 1, 3), new DateOnly(2000, 1, 5), new DateOnly(2000, 1, 7) };

        // act

        var actual = timeline.GetPaymentDates().Unwrap();

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPaymentDates_TwiceAWeekForTwoWeeks_ReturnsCorrectDates() {
        // arrange

        var period = TimePeriod.Create(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 14)).Unwrap();
        var frequency = Frequency.Create(TimeUnit.Weeks, 2, 1).Unwrap();
        var timeline = Timeline.Create(period, frequency).Unwrap();

        var expected = new[] { new DateOnly(2000, 1, 4), new DateOnly(2000, 1, 7), new DateOnly(2000, 1, 11), new DateOnly(2000, 1, 14) };

        // act

        var actual = timeline.GetPaymentDates().Unwrap();

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPaymentDates_TwiceADayForTwoDays_ReturnsCorrectDates() {
        // arrange

        var period = TimePeriod.Create(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 2)).Unwrap();
        var frequency = Frequency.Create(TimeUnit.Days, 2, 1).Unwrap();
        var timeline = Timeline.Create(period, frequency).Unwrap();

        var expected = new[] { new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 2), new DateOnly(2000, 1, 2) };

        // act

        var actual = timeline.GetPaymentDates().Unwrap();

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPaymentDates_FiveTimesAYearForThreeYears_ReturnsCorrectDateCount() {
        // arrange

        var period = TimePeriod.Create(new DateOnly(2020, 1, 1), new DateOnly(2023, 1, 1)).Unwrap();
        var frequency = Frequency.Create(TimeUnit.Years, 5, 1).Unwrap();
        var timeline = Timeline.Create(period, frequency).Unwrap();

        var expected = 15;

        // act

        var actual = timeline.GetPaymentDates().Unwrap().Count();

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPaymentDates_FiveTimesAYearForAlmostThreeYears_ReturnsCorrectDateCount() {
        // arrange

        var period = TimePeriod.Create(new DateOnly(2020, 1, 1), new DateOnly(2022, 12, 31)).Unwrap(); // until last day of dec 2022
        var frequency = Frequency.Create(TimeUnit.Years, 5, 1).Unwrap();
        var timeline = Timeline.Create(period, frequency).Unwrap();

        var expected = 15;

        // act

        var actual = timeline.GetPaymentDates().Unwrap().Count();

        // assert

        Assert.Equal(expected, actual);
    }
}

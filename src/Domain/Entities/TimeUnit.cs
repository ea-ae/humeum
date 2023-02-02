using Domain.Common;

namespace Domain.Entities;

public class TimeUnit : EnumerationEntity {
    public static readonly TimeUnit Hours = new("HOURS", delegate(DateTime start, DateTime end) {
        var timeSpan = end - start;
        return (int)timeSpan.TotalHours;
    });

    public static readonly TimeUnit Days = new("DAYS", delegate (DateTime start, DateTime end) {
        var timeSpan = end - start;
        return (int)timeSpan.TotalDays;
    });

    public static readonly TimeUnit Weeks = new("WEEKS", delegate (DateTime start, DateTime end) {
        var timeSpan = end - start;
        return (int)(timeSpan.TotalDays / 7);
    });

    public static readonly TimeUnit Months = new("MONTHS", delegate (DateTime start, DateTime end) {
        var timeSpan = end - start;
        int years = end.Year - start.Year;
        int months = end.Month - start.Month;
        bool isLastDayOfMonth = end.Day == DateTime.DaysInMonth(end.Year, end.Month);

        if (start.AddYears(years).AddMonths(months) > end && !isLastDayOfMonth) {
            return years * 12 + months - 1; // payments occur on the same date every month
        }
        return years * 12 + months;
    });

    public static readonly TimeUnit Years = new("YEARS", delegate (DateTime start, DateTime end) {
        int years = end.Year - start.Year;
        bool isLastDayOfMonth = end.Day == DateTime.DaysInMonth(end.Year, end.Month);

        if (start.AddYears(years) > end && !isLastDayOfMonth) {
            return years - 1; // didn't reach payment date on the last year
        }
        return years;
    });

    public Func<DateTime, DateTime, int> InTimeSpan { get; init; } = null!;
    
    private TimeUnit(string code, Func<DateTime, DateTime, int> unitsInTimeSpanDelegate) : base(code) {
        InTimeSpan = unitsInTimeSpanDelegate;
    }

    private TimeUnit(string name, string code) : base(name, code) {
        InTimeSpan = GetAll<TimeUnit>().First(t => t.Code == code).InTimeSpan; // assign delegate to EF entities
    }
}

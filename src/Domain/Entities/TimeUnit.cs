using Domain.Common;

namespace Domain.Entities;

public class TimeUnit : EnumerationEntity {
    public static readonly TimeUnit Hours = new(1, "HOURS", delegate(DateTime start, DateTime end) {
        var timeSpan = end - start;
        return (int)timeSpan.TotalHours;
    });

    public static readonly TimeUnit Days = new(2, "DAYS", delegate (DateTime start, DateTime end) {
        var timeSpan = end - start;
        return (int)timeSpan.TotalDays;
    });

    public static readonly TimeUnit Weeks = new(3, "WEEKS", delegate (DateTime start, DateTime end) {
        var timeSpan = end - start;
        return (int)(timeSpan.TotalDays / 7);
    });

    public static readonly TimeUnit Months = new(4, "MONTHS", delegate (DateTime start, DateTime end) {
        var timeSpan = end - start;
        int years = end.Year - start.Year;
        int months = end.Month - start.Month;
        bool isLastDayOfMonth = end.Day == DateTime.DaysInMonth(end.Year, end.Month);

        if (start.AddYears(years).AddMonths(months) > end && !isLastDayOfMonth) {
            return years * 12 + months - 1; // payments occur on the same date every month
        }
        return years * 12 + months;
    });

    public static readonly TimeUnit Years = new(5, "YEARS", delegate (DateTime start, DateTime end) {
        int years = end.Year - start.Year;
        bool isLastDayOfMonth = end.Day == DateTime.DaysInMonth(end.Year, end.Month);

        if (start.AddYears(years) > end && !isLastDayOfMonth) {
            return years - 1; // didn't reach payment date on the last year
        }
        return years;
    });

    public Func<DateTime, DateTime, int> InTimeSpan { get; init; } = null!;
    
    private TimeUnit(int id, string code, Func<DateTime, DateTime, int> unitsInTimeSpanDelegate) : base(id, code) {
        InTimeSpan = unitsInTimeSpanDelegate;
    }

    #pragma warning disable IDE0051 // Remove unused private members
    /// <summary>
    /// Private constructor for EF that assigns a proper delegate
    /// </summary>
    /// <param name="code"></param>
    /// <param name="name"></param>
    private TimeUnit(string code, string name) : base(code, name) {
        var timeUnit = GetAll<TimeUnit>().First(t => t.Code == code);
        InTimeSpan = timeUnit.InTimeSpan; // assign delegate to EF entities
    }
    #pragma warning restore IDE0051 // Remove unused private members
}

using Domain.Common;

namespace Domain.TransactionAggregate.ValueObjects;

public class TimeUnit : Enumeration {
    public static readonly TimeUnit Days = new(2, "DAYS", (DateOnly start, DateOnly end) => {
        var timeSpan = end.ToDateTime(TimeOnly.MinValue) - start.ToDateTime(TimeOnly.MinValue);
        return (int)timeSpan.TotalDays + 1;
    });

    public static readonly TimeUnit Weeks = new(3, "WEEKS", (DateOnly start, DateOnly end) => {
        var timeSpan = end.ToDateTime(TimeOnly.MinValue) - start.ToDateTime(TimeOnly.MinValue);
        return (int)(timeSpan.TotalDays / 7) + 1;
    });

    public static readonly TimeUnit Months = new(4, "MONTHS", (DateOnly start, DateOnly end) => {
        int years = end.Year - start.Year;
        int months = end.Month - start.Month;
        bool isLastDayOfMonth = end.Day == DateTime.DaysInMonth(end.Year, end.Month);

        if (start.AddYears(years).AddMonths(months) > end && !isLastDayOfMonth) {
            return years * 12 + months; // payments occur on the same date every month
        }
        return years * 12 + months + 1;
    });

    public static readonly TimeUnit Years = new(5, "YEARS", (DateOnly start, DateOnly end) => {
        int years = end.Year - start.Year;
        bool isLastDayOfMonth = end.Day == DateTime.DaysInMonth(end.Year, end.Month);

        if (start.AddYears(years) > end && !isLastDayOfMonth) {
            return years; // didn't reach payment date on the last year
        }
        return years + 1;
    });

    public Func<DateOnly, DateOnly, int> InTimeSpan { get; init; } = null!;

#pragma warning disable IDE0051 // Remove unused private members
    TimeUnit(string code, string name) : base(code, name) {
        InTimeSpan = GetTimeSpanForCode(code);
    }

    TimeUnit(string code) : base(code) {
        InTimeSpan = GetTimeSpanForCode(code);
    }
#pragma warning restore IDE0051 // Remove unused private members

    TimeUnit(int id, string code, Func<DateOnly, DateOnly, int> unitsInTimeSpanDelegate) : base(id, code) {
        InTimeSpan = unitsInTimeSpanDelegate;
    }

    static Func<DateOnly, DateOnly, int> GetTimeSpanForCode(string code) {
        // assigns proper delegate during EF Core entity construction
        var timeUnit = GetAll<TimeUnit>().First(t => t.Code == code);
        return timeUnit.InTimeSpan;
    }
}

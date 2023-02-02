using Domain.Common;

namespace Domain.Entities;

public class Transaction : TimestampedEntity {
    public decimal Amount { get; set; }
    public required TransactionType Type { get; set; }

    public required TransactionTimescale Timescale { get; set; }
    public bool PerTimescale { get; set; } // e.g. "every second week" (false) vs "twice a week" (true)
    public DateTime PaymentStart { get; set; }
    public DateTime? PaymentEnd { get; set; }

    private Transaction() { }

    public int TotalTransactionCount {
        get {
            if (PaymentEnd is null) {
                return 1;
            }

            TimeSpan timespan = (TimeSpan)(PaymentEnd - PaymentStart);
            int count = 0;
            int years = ((DateTime)PaymentEnd).Year - PaymentStart.Year;
            int months = ((DateTime)PaymentEnd).Month - PaymentStart.Month;

            int daysInEndDate = DateTime.DaysInMonth(((DateTime)PaymentEnd).Year, ((DateTime)PaymentEnd).Month);
            bool isLastDayOfMonth = ((DateTime)PaymentEnd).Day == daysInEndDate;

            switch (Timescale.Code) {
                case "HOURS":
                    count = (int)timespan.TotalHours;
                    break;
                case "DAYS":
                    count = (int)timespan.TotalDays;
                    break;
                case "WEEKS":
                    count = (int)(timespan.TotalDays / 7);
                    break;
                case "MONTHS":
                    count += years * 12;
                    count += months;

                    if (PaymentStart.AddYears(years).AddMonths(months) > (DateTime)PaymentEnd && !isLastDayOfMonth) {
                        count -= 1; // payments occur on the same date every month
                    }
                    break;
                case "YEARS":
                    count += years;

                    if (PaymentStart.AddYears(years) > (DateTime)PaymentEnd && !isLastDayOfMonth) {
                        count -= 1; // didn't reach payment date on the last year
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(Timescale.Code, $"Unexpected time interval code: {Timescale.Code}");
            }

            return count + 1; // first transaction is at the start date, so add 1
        }
    }

    public decimal TotalTransactionAmount => Amount * TotalTransactionCount;
}

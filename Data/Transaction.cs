namespace Domain;

public class Transaction {
    public int Id { get; set; }
    public DateTime Start { get; set; } = DateTime.UtcNow;
    public DateTime? End { get; set; } = null;

    public decimal Amount { get; set; }
    public required TransactionType Type { get; set; }

    public required TransactionTimescale Timescale { get; set; }
    public bool PerTimescale { get; set; } // e.g. "every second week" (false) vs "twice a week" (true)
    public DateTime PaymentStart { get; set; }
    public DateTime? PaymentEnd { get; set; }

    public int TotalTransactionCount {
        get {
            if (PaymentEnd is null) {
                return 1;
            }

            TimeSpan timespan = (TimeSpan)(PaymentEnd - PaymentStart);
            int count = 0;
            int years;
            int months;
            
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
                    years = (((DateTime)PaymentEnd).Year - PaymentStart.Year) * 12;
                    count += years;
                    months = (((DateTime)PaymentEnd).Month - PaymentStart.Month);
                    count += months;

                    if (PaymentStart.AddYears(years).AddMonths(months) > (DateTime)PaymentEnd) {
                        count -= 1; // payments occur on the same date every month
                    }

                    break;
                case "YEARS":
                    years = (((DateTime)PaymentEnd).Year - PaymentStart.Year);
                    count += years;

                    if (PaymentStart.AddYears(years) > (DateTime)PaymentEnd) {
                        count -= 1; // didn't reach payment date on the last year
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(Timescale.Code, $"Unexpected time interval code: {Timescale.Code}");
            }

            return count + 1; // first transaction is at the start date, so add 1
        }
    }
}

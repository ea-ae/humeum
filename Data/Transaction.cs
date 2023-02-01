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
            int count = (int)(Timescale.Code switch {
                "HOURS" => timespan.TotalHours,
                "DAYS" => timespan.TotalDays,
                "WEEKS" => timespan.TotalDays / 7,
                _ => throw new ArgumentOutOfRangeException($"Unexpected time interval code: {Timescale.Code}"),
            });
            count++; // first transaction is at the start date

            return count;
        }
    }
}

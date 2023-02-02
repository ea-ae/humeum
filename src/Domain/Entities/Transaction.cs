using Domain.Common;

namespace Domain.Entities;

public class Transaction : TimestampedEntity {
    private decimal _amount;
    public decimal Amount {
        get => _amount;
        private set {
            if (_amount <= 0) {
                throw new ArgumentException("Amount must be greater than zero.");
            }
            _amount = value;
        }
    }

    private TransactionFrequency? _frequency;
    public TransactionFrequency? Frequency { 
        get => _frequency;
        private set {
            if (value is null && PaymentEnd is not null) {
                throw new InvalidOperationException("Cannot set frequency to null with a payment end date.");
            } else if (value is not null && PaymentEnd is null) {
                throw new InvalidOperationException("Cannot assign a frequency when there is no payment end date.");
            }
            _frequency = value;
        }
    }

    public TransactionType Type { get; private set; } = null!;

    private DateTime _paymentStart;
    public DateTime PaymentStart {
        get => _paymentStart;
        private set {
            if (PaymentEnd is not null && PaymentEnd >= PaymentStart) {
                throw new ArgumentException("Payment end date must be after start date.");
            }
            _paymentStart = value;
        }
    }

    private DateTime? _paymentEnd;

    public DateTime? PaymentEnd {
        get => _paymentEnd;
        private set {
            if (PaymentEnd >= PaymentStart) {
                throw new ArgumentException("Payment end date must be after start date.");
            }
            _paymentEnd = value;
        }
    }

    public Transaction(decimal amount,
                       TransactionType type,
                       TransactionFrequency frequency,
                       DateTime paymentStart,
                       DateTime paymentEnd) {
        Amount = amount;
        Type = type;
        Frequency = frequency;
        PaymentStart = paymentStart;
        PaymentEnd = paymentEnd;
    }

    public Transaction(decimal amount,
                       TransactionType type,
                       DateTime paymentStart) {
        Amount = amount;
        Type = type;
        PaymentStart = paymentStart;
    }

    private Transaction() { }

    public int TotalTransactionCount {
        get {
            if (PaymentEnd is null || Frequency is null) {
                return 1;
            }

            TimeSpan timespan = (TimeSpan)(PaymentEnd - PaymentStart);
            int count = 0;
            int years = ((DateTime)PaymentEnd).Year - PaymentStart.Year;
            int months = ((DateTime)PaymentEnd).Month - PaymentStart.Month;

            int daysInEndDate = DateTime.DaysInMonth(((DateTime)PaymentEnd).Year, ((DateTime)PaymentEnd).Month);
            bool isLastDayOfMonth = ((DateTime)PaymentEnd).Day == daysInEndDate;

            switch (Frequency.Unit.Code) {
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
                    throw new ArgumentOutOfRangeException(
                        Frequency.Unit.Code, $"Unexpected time interval code: {Frequency.Unit.Code}");
            }

            return count + 1; // first transaction is at the start date, so add 1
        }
    }

    public decimal TotalTransactionAmount => Amount * TotalTransactionCount;
}

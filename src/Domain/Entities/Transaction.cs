using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Transaction : TimestampedEntity {
    decimal _amount;
    public decimal Amount {
        get => _amount;
        private set {
            if (value <= 0) {
                throw new ArgumentException("Amount must be greater than zero.");
            }
            _amount = value;
        }
    }

    public TransactionType Type { get; private set; } = null!;

    Frequency? _frequency;
    public Frequency? Frequency { 
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

    //TimePeriod _paymentPeriod;
    //public TimePeriod paymentPeriod {
    //    get => _paymentPeriod;
    //}

    DateTime _paymentStart;
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
            if (value is null && Frequency is not null) {
                throw new InvalidOperationException("Cannot set payment end date to null when there is a frequency.");
            } else if (value is not null && Frequency is null) {
                throw new InvalidOperationException("Cannot assign a payment end date when there is no frequency.");
            } else if (PaymentEnd >= PaymentStart) {
                throw new ArgumentException("Payment end date must be after the start date.");
            }
            _paymentEnd = value;
        }
    }

    public Transaction(decimal amount,
                       TransactionType type,
                       DateTime paymentStart) {
        Amount = amount;
        Type = type;
        PaymentStart = paymentStart;
    }

    public Transaction(decimal amount,
                       TransactionType type,
                       DateTime paymentStart,
                       DateTime paymentEnd,
                       Frequency frequency) {
        Amount = amount;
        Type = type;
        _paymentStart = paymentStart;
        _frequency = frequency;
        PaymentEnd = paymentEnd;
    }

    private Transaction() { }

    public int TotalTransactionCount {
        get {
            if (PaymentEnd is null || Frequency is null) {
                return 1;
            }

            int count = Frequency.Unit.InTimeSpan(PaymentStart, (DateTime)PaymentEnd);
            return count + 1; // first transaction is at the start date, so add 1
        }
    }

    public decimal TotalTransactionAmount => Amount * TotalTransactionCount;
}

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
            if (value is null && PaymentPeriod.IsRecurring) {
                throw new InvalidOperationException("Cannot set frequency to null with a payment end date.");
            } else if (value is not null && !PaymentPeriod.IsRecurring) {
                throw new InvalidOperationException("Cannot assign a frequency when there is no payment end date.");
            }
            _frequency = value;
        }
    }

    TimePeriod _paymentPeriod;
    public TimePeriod PaymentPeriod {
        get => _paymentPeriod;
        set {
            if (value.End is null && Frequency is not null) {
                throw new InvalidOperationException("Cannot set payment end date to null when there is a frequency.");
            } else if (value.End is not null && Frequency is null) {
                throw new InvalidOperationException("Cannot assign a payment end date when there is no frequency.");
            }
            _paymentPeriod = value;
        }
    }

    public Transaction(decimal amount, TransactionType type, TimePeriod paymentPeriod) {
        Amount = amount;
        Type = type;
        _paymentPeriod = paymentPeriod;
    }

    public Transaction(decimal amount, TransactionType type, TimePeriod paymentPeriod, Frequency frequency) {
        Amount = amount;
        Type = type;
        _paymentPeriod = paymentPeriod;
        _frequency = frequency;
    }

    private Transaction() { }

    public int TotalTransactionCount {
        get {
            if (!PaymentPeriod.IsRecurring || Frequency is null) {
                return 1;
            }

            // todo: change this param/arg to just the type itself
            int count = Frequency.Unit.InTimeSpan(PaymentPeriod.Start, (DateTime)PaymentPeriod.End!);
            return count;
        }
    }

    public decimal TotalTransactionAmount => Amount * TotalTransactionCount;
}

using Domain.Common;
using Domain.TransactionAggregate.ValueObjects;

namespace Domain.TransactionAggregate;

public class Transaction : TimestampedEntity
{
    decimal _amount; // value object? encapsulate in constructor or nah?
    public decimal Amount
    {
        get => _amount;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }
            _amount = value;
        }
    }

    public TransactionType Type { get; private set; } = null!;

    public Timeline PaymentTimeline { get; private set; } = null!;

    public Transaction(decimal amount, TransactionType type, Timeline paymentTimeline)
    {
        Amount = amount;
        Type = type;
        PaymentTimeline = paymentTimeline;
    }

    private Transaction() { }

    public int TotalTransactionCount
    {
        get
        {
            if (!PaymentTimeline.Period.IsRecurring || PaymentTimeline.Frequency is null)
            {
                return 1;
            }

            // todo: change this param/arg to just the type itself
            int count = PaymentTimeline.Frequency.Unit.InTimeSpan(PaymentTimeline.Period.Start,
                                                                  (DateTime)PaymentTimeline.Period.End!);
            return count;
        }
    }

    public decimal TotalTransactionAmount => Amount * TotalTransactionCount;
}

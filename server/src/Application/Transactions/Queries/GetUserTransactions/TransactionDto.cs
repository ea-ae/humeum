namespace Application.Transactions.Queries.GetUserTransactions;

public record TransactionDto {
    public int Id { get; init; }
    public decimal Amount { get; init; }

    public string? PaymentTimelineFrequencyUnitName { get; init; }
    public string? PaymentTimelineFrequencyUnitCode { get; init; }
    public int? PaymentTimelineFrequencyTimesPerUnit { get; init; }

    public DateOnly PaymentTimelinePeriodStart { get; init; }
    public DateOnly? PaymentTimelinePeriodEnd { get; init; }
}

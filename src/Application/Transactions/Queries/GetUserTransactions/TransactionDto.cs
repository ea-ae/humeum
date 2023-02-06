namespace Application.Transactions.Queries.GetUserTransactions;

public record TransactionDto {
    public int Id { get; init; }
    public decimal Amount { get; init; }

    public string? PaymentTimelineFrequencyUnitName { get; init; }
    public string? PaymentTimelineFrequencyUnitCode { get; init; }
    public int? PaymentTimelineFrequencyTimesPerUnit { get; init; }

    public DateTime PaymentTimelinePeriodStart { get; init; }
    public DateTime? PaymentTimelinePeriodEnd { get; init; }
}

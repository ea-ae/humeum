namespace Application.Transactions.Queries;

public record TransactionDto {
    public record BriefTransactionCategory {
        public required int Id { get; init; }
        public required string Name { get; init; }
    }

    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Amount { get; init; }

    public required string TypeName { get; init; }
    public required string TypeCode { get; init; }

    public required int TaxSchemeId { get; init; }
    public required int? AssetId { get; init; }
    public required IEnumerable<BriefTransactionCategory> Categories { get; init; }

    public required string? PaymentTimelineFrequencyTimeUnitName { get; init; }
    public required string? PaymentTimelineFrequencyTimeUnitCode { get; init; }
    public required int? PaymentTimelineFrequencyTimesPerCycle { get; init; }
    public required int? PaymentTimelineFrequencyUnitsInCycle { get; init; }

    public required DateOnly PaymentTimelinePeriodStart { get; init; }
    public required DateOnly? PaymentTimelinePeriodEnd { get; init; }
}

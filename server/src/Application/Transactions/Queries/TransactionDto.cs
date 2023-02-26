﻿namespace Application.Transactions.Queries;

public record TransactionDto {
    public record BriefTransactionCategory {
        public required int Id { get; init; }
        public required string Name { get; init; }
    }

    public required int Id { get; init; }
    public required decimal Amount { get; init; }

    public required int TaxSchemeId { get; init; }
    public required int? AssetId { get; init; }
    public required IEnumerable<BriefTransactionCategory> Categories { get; init; }

    public required string? PaymentTimelineFrequencyUnitName { get; init; }
    public required string? PaymentTimelineFrequencyUnitCode { get; init; }
    public required int? PaymentTimelineFrequencyTimesPerUnit { get; init; }

    public required DateOnly PaymentTimelinePeriodStart { get; init; }
    public required DateOnly? PaymentTimelinePeriodEnd { get; init; }
}

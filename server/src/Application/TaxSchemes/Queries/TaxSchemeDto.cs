namespace Application.TaxSchemes.Queries;

public record TaxSchemeDto {
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal TaxRate { get; init; }

    public required decimal? IncentiveSchemeTaxRefundRate { get; init; }
    public required int? IncentiveSchemeMinAge { get; init; }
    public required decimal? IncentiveSchemeMaxIncomePercentage { get; init; }
    public required int? IncentiveSchemeMaxApplicableIncome { get; init; }
}

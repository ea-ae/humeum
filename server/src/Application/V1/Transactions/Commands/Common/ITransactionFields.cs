﻿namespace Application.V1.Transactions.Commands.Common;

public interface ITransactionFields
{
    public int Profile { get; init; }

    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal? Amount { get; init; }
    public string Type { get; init; }
    public DateTime? PaymentStart { get; init; }
    public int? TaxScheme { get; init; }
    public int? Asset { get; init; }

    public DateTime? PaymentEnd { get; init; }
    public string? TimeUnit { get; init; }
    public int? TimesPerCycle { get; init; }
    public int? UnitsInCycle { get; init; }
}

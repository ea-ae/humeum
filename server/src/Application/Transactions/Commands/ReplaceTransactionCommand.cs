using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.AssetAggregate;
using Domain.Common.Interfaces;
using Domain.Common.Models;
using Domain.TaxSchemeAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

using MediatR;

namespace Application.Transactions.Commands;

public record ReplaceTransactionCommand : ICommand<IResult<None>> {
    [Required] public required int Profile { get; init; }
    [Required] public required int Transaction { get; init; }

    public string? Name { get; init; }
    public string? Description { get; init; }
    [Required] public decimal? Amount { get; init; }
    [Required] public required string Type { get; init; }
    [Required] public DateOnly? PaymentStart { get; init; }
    [Required] public required int? TaxScheme { get; init; }
    public int? Asset { get; init; }

    public DateOnly? PaymentEnd { get; init; }
    public string? TimeUnit { get; init; }
    public int? TimesPerCycle { get; init; }
    public int? UnitsInCycle { get; init; }
}

public class ReplaceTransactionCommandHandler : ICommandHandler<ReplaceTransactionCommand, IResult<None>> {
    readonly IAppDbContext _context;

    public ReplaceTransactionCommandHandler(IAppDbContext context) {
        _context = context;
    }

    public async Task<IResult<None>> Handle(ReplaceTransactionCommand request, CancellationToken token = default) {
        // validation

        List<object?> recurringTransactionFields = new() { // fields required for recurrent transactions
            request.PaymentEnd,
            request.TimeUnit,
            request.TimesPerCycle,
            request.UnitsInCycle
        };
        bool isRecurringTransaction = recurringTransactionFields.AssertOptionalFieldSetValidity();

        var taxScheme = _context.TaxSchemes.FirstOrDefault(ts => ts.Id == request.TaxScheme && ts.DeletedAt == null);
        if (taxScheme is null) {
            return Result<None>.Fail(new NotFoundValidationException(typeof(TaxScheme)));
        }

        Asset? asset = null;
        if (request.Asset is not null) {
            asset = _context.Assets.FirstOrDefault(a => (a.ProfileId == request.Profile || a.ProfileId == null)
                                                        && a.Id == request.Asset
                                                        && a.DeletedAt == null);
            if (asset is null) {
                return Result<None>.Fail(new NotFoundValidationException(typeof(Asset)));
            }
        }

        // handling

        var transaction = _context.Transactions.FirstOrDefault(t => t.Id == request.Transaction
                                                                    && t.ProfileId == request.Profile
                                                                    && t.DeletedAt == null);
        if (transaction is null) {
            return Result<None>.Fail(new NotFoundValidationException(typeof(Transaction)));
        }

        var transactionType = _context.GetEnumerationEntityByCode<TransactionType>(request.Type);
        Timeline paymentTimeline;

        // update fields

        if (isRecurringTransaction) {
            var timeUnit = _context.GetEnumerationEntityByCode<TimeUnit>(request.TimeUnit!);

            var paymentPeriod = new TimePeriod((DateOnly)request.PaymentStart!, (DateOnly)request.PaymentEnd!);
            var paymentFrequency = new Frequency(timeUnit, (int)request.TimesPerCycle!, (int)request.UnitsInCycle!);

            paymentTimeline = new Timeline(paymentPeriod, paymentFrequency);

            transaction.Replace(request.Name, request.Description, (decimal)request.Amount!, transactionType,
                                paymentTimeline, taxScheme, asset);
        } else {
            paymentTimeline = new Timeline(new TimePeriod((DateOnly)request.PaymentStart!));
            transaction.Replace(request.Name, request.Description, (decimal)request.Amount!, transactionType,
                                paymentTimeline, taxScheme, asset);
        }

        await _context.SaveChangesWithHardDeletionAsync(token);

        return Result<None>.Ok(None.Value);
    }
}

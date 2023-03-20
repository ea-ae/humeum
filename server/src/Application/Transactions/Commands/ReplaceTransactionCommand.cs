using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Transactions.Queries;

using AutoMapper;

using Domain.AssetAggregate;
using Domain.TaxSchemeAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Commands;

public record ReplaceTransactionCommand : ICommand {
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

public class ReplaceTransactionCommandHandler : ICommandHandler<ReplaceTransactionCommand> {
    readonly IAppDbContext _context;

    public ReplaceTransactionCommandHandler(IAppDbContext context) {
        _context = context;
    }

    public async Task<Unit> Handle(ReplaceTransactionCommand request, CancellationToken token) {
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
            throw new NotFoundValidationException(typeof(TaxScheme));
        }

        Asset? asset = null;
        if (request.Asset is not null) {
            asset = _context.Assets.FirstOrDefault(a => (a.ProfileId == request.Profile || a.ProfileId == null)
                                                        && a.Id == request.Asset
                                                        && a.DeletedAt == null);
            if (asset is null) {
                throw new NotFoundValidationException(typeof(Asset));
            }
        }

        // handling

        var transaction = _context.Transactions.FirstOrDefault(t => t.Id == request.Transaction
                                                                    && t.ProfileId == request.Profile
                                                                    && t.DeletedAt == null);
        if (transaction is null) {
            throw new NotFoundValidationException(typeof(Transaction));
        }

        // detach
        _context.Entry(transaction).State = EntityState.Detached;
        _context.Entry(transaction.PaymentTimeline).State = EntityState.Detached;
        _context.Entry(transaction.PaymentTimeline.Period).State = EntityState.Detached;
        if (transaction.PaymentTimeline.Frequency is not null) {
            _context.Entry(transaction.PaymentTimeline.Frequency).State = EntityState.Detached;
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

        // workaround for EF bug with immutable owned entities

        _context.Update(transaction);
        _context.Update(transaction.PaymentTimeline);
        //_context.Update(transaction.PaymentTimeline.Frequency);
        await _context.SaveChangesAsync(token);

        return Unit.Value;
    }
}

using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.Common.Models;
using Domain.TaxSchemeAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

namespace Application.Transactions.Commands;

public record AddTransactionCommand : ICommand<IResult<int>> {
    [Required] public required int Profile { get; init; }

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

public class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand, IResult<int>> {
    readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<IResult<int>> Handle(AddTransactionCommand request, CancellationToken token = default) {
        // create a builder to collect validation errors

        var builder = new Result<int>.Builder();

        // validate that the fields for recurring transactions were provided either fully or not at all

        List<object?> recurringTransactionFields = new() {
            request.PaymentEnd,
            request.TimeUnit,
            request.TimesPerCycle,
            request.UnitsInCycle
        };
        var isRecurringTransaction = recurringTransactionFields.AssertOptionalFieldSetValidity();
        builder.AddResultErrors(isRecurringTransaction);

        // validate that the tax scheme exists

        var taxSchemeExists = _context.TaxSchemes.Any(ts => ts.Id == request.TaxScheme && ts.DeletedAt == null);
        if (!taxSchemeExists) {
            builder.AddError(new NotFoundValidationException(typeof(TaxScheme)));
        }

        // validate that the asset exists if one was specified

        if (request.Asset is not null) {
            var assetExists = _context.Assets.Any(a => (a.ProfileId == request.Profile || a.ProfileId == null)
                                                       && a.Id == request.Asset
                                                       && a.DeletedAt == null);
            if (!assetExists) {
                builder.AddError(new NotFoundValidationException(typeof(Transaction)));
            }
        }

        // if the fields were specified partially, transaction creation cannot proceed; return failed result early

        if (isRecurringTransaction.Failure) {
            return (IResult<int>)builder.Build();
        }

        // get the transaction type

        var transactionType = _context.GetEnumerationEntityByCode<TransactionType>(request.Type);
        builder.AddResultErrors(transactionType);

        // create a payment timeline value object for the transaction; return failed result early if needed

        Timeline paymentTimeline;

        if (isRecurringTransaction.Unwrap()) {
            var paymentPeriod = new TimePeriod((DateOnly)request.PaymentStart!, (DateOnly)request.PaymentEnd!);

            var timeUnit = _context.GetEnumerationEntityByCode<TimeUnit>(request.TimeUnit!);
            if (timeUnit.Failure) {
                return (IResult<int>)builder.AddResultErrors(timeUnit).Build();
            }

            var paymentFrequency = new Frequency(timeUnit.Unwrap(), (int)request.TimesPerCycle!, (int)request.UnitsInCycle!);
            paymentTimeline = new Timeline(paymentPeriod, paymentFrequency);
        } else {
            paymentTimeline = new Timeline(new TimePeriod((DateOnly)request.PaymentStart!));
        }

        // if there were any validation errors, return them

        if (builder.HasErrors) {
            return (IResult<int>)builder.Build();
        }

        // build the transaction object

        var transaction = Transaction.Create(request.Name, request.Description, (decimal)request.Amount!, transactionType.Unwrap(),
                                             paymentTimeline, request.Profile!, (int)request.TaxScheme!, request.Asset);

        // persist and return the transaction object ID or validation errors

        return await transaction.ThenAsync<int>(async value => {
            _context.Transactions.Add(value);
            await _context.SaveChangesAsync(token);
            return Result<int>.Ok(value.Id);
        });
    }
}

using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Transactions.Commands.Common;

using Domain.AssetAggregate;
using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.Common.Models;
using Domain.TaxSchemeAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

namespace Application.Transactions.Commands;

public record ReplaceTransactionCommand : ICommand<IResult<None>>, ITransactionFields {
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
        // create a builder to collect validation errors

        var builder = new Result<None>.Builder();

        // validate that the specified transaction exists

        var transaction = _context.Transactions.FirstOrDefault(t => t.Id == request.Transaction
                                                                    && t.ProfileId == request.Profile
                                                                    && t.DeletedAt == null);
        if (transaction is null) {
            builder.AddError(new NotFoundValidationException(typeof(Transaction)));
        }

        // validate transaction fields

        var validationResult = request.ValidateTransactionFields(_context);
        builder.AddResultErrors(validationResult);

        // if there are any validation errors at this point, return them

        if (builder.HasErrors) {
            return (IResult<None>)builder.Build();
        }

        // update the transaction

        var (transactionType, paymentTimeline) = validationResult.Unwrap();
        var result = transaction!.Replace(request.Name, request.Description, (decimal)request.Amount!, transactionType,
                                          paymentTimeline, (int)request.TaxScheme!, request.Asset);

        return await result.ThenAsync<None>(async _ => {
            await _context.SaveChangesWithHardDeletionAsync(token);
            return Result<None>.Ok(None.Value);
        });
    }
}

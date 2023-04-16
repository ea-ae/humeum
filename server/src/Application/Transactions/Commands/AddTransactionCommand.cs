using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;
using Application.Transactions.Commands.Common;

using Domain.TransactionAggregate;

using Shared.Interfaces;
using Shared.Models;

namespace Application.Transactions.Commands;

public record AddTransactionCommand : ICommand<IResult<int, IBaseException>>, ITransactionFields {
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

public class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand, IResult<int, IBaseException>> {
    readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<IResult<int, IBaseException>> Handle(AddTransactionCommand request, CancellationToken token = default) {
        // validate the transaction creation request fields

        var validationResult = request.ValidateTransactionFields(_context);

        if (validationResult.Failure) {
            return Result<int, IBaseException>.Fail(validationResult.GetErrors());
        }

        var (transactionType, paymentTimeline) = validationResult.Unwrap();

        // build the transaction object

        var transaction = Transaction.Create(request.Name, request.Description, (decimal)request.Amount!, transactionType,
                                             paymentTimeline, request.Profile!, (int)request.TaxScheme!, request.Asset);

        // persist and return the transaction object ID or validation errors

        return await transaction.ThenAsync<int, IBaseException>(async value => {
            _context.Transactions.Add(value);
            await _context.SaveChangesAsync(token);
            return Result<int, IBaseException>.Ok(value.Id);
        });
    }
}

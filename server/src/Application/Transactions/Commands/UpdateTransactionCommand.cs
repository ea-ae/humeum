//using System.ComponentModel.DataAnnotations;

//using Application.Common.Exceptions;
//using Application.Common.Extensions;
//using Application.Common.Interfaces;
//using Application.Transactions.Queries;

//using AutoMapper;

//using Domain.TransactionAggregate;
//using Domain.TransactionAggregate.ValueObjects;

//namespace Application.Transactions.Commands;

//public record UpdateTransactionCommand : ICommand<TransactionDto> {
//    [Required] public required int User { get; init; }
//    [Required] public required int Profile { get; init; }
//    [Required] public required int Transaction { get; init; }

//    public string? Name { get; init; }
//    public string? Description { get; init; }
//    [Required] public decimal Amount { get; init; }
//    public string? Type { get; init; }
//    public DateOnly? PaymentStart { get; init; }
//    public int? TaxScheme { get; init; }
//    public int? Asset { get; init; }

//    public DateOnly? PaymentEnd { get; init; }
//    public string? TimeUnit { get; init; }
//    public int? TimesPerCycle { get; init; }
//    public int? UnitsInCycle { get; init; }
//}

//public class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand, TransactionDto> {
//    readonly IAppDbContext _context;
//    readonly IMapper _mapper;

//    public UpdateTransactionCommandHandler(IAppDbContext context, IMapper mapper) {
//        _context = context;
//        _mapper = mapper;
//    }

//    public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken) {
//        var transaction = _context.Transactions.FirstOrDefault(t => t.Id == request.Transaction
//                                                                    && t.ProfileId == request.Profile
//                                                                    && t.DeletedAt == null);
//        if (transaction is null) {
//            throw new NotFoundValidationException(typeof(Transaction));
//        }

//        // update fields

//        if (request.Name is not null) {
//            transaction.Name = request.Name;
//        }
//        if (request.Description is not null) {
//            transaction.Description = request.Description;
//        }
//        if (request.Amount is not null) {
//            transaction.Amount = (decimal)request.Amount;
//        }
//        if (request.Type is not null) {
//            var transactionType = _context.GetEnumerationEntityByCode<TransactionType>(request.Type);
//            transaction.UpdateType(transactionType);
//        }

//        List<object?> recurringTransactionFields = new() { // fields required for recurrent transactions
//            request.PaymentEnd,
//            request.TimeUnit,
//            request.TimesPerCycle,
//            request.UnitsInCycle
//        };
//        bool isRecurringTransaction = recurringTransactionFields.AssertOptionalFieldSetValidity();

//        if (request.PaymentStart is not null || isRecurringTransaction) {
//            var paymentStart = request.PaymentStart ?? transaction.PaymentTimeline.Period.Start;

//            if (isRecurringTransaction) {
//                var paymentEnd = request.PaymendEnd ?? transaction.PaymentTimeline.Period.End ?? throw new 
//            } else {
//                transaction.PaymentTimeline = new Timeline(new TimePeriod(paymentStart));
//            }
//        }

//        await _context.SaveChangesAsync();

//        return _mapper.Map<TransactionDto>(transaction);
//    }
//}

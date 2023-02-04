using Domain.Entities;

namespace Application.Transactions.Queries.GetUserTransactions;

public class TransactionDto {
    public int Id { get; set; }
    public decimal Amount { get; set; }
    
    public int? TimesPerUnit { get; set; }
    public string? Unit { get; set; }
    
    public DateTime PaymentStart { get; set; }
    public DateTime? PaymentEnd { get; set; }

    public TransactionDto(Transaction transaction) { // temporary; until we add AutoMapper
        Id = transaction.Id;
        Amount = transaction.Amount;

        TimesPerUnit = transaction.Frequency?.TimesPerUnit;
        Unit = transaction.Frequency?.Unit.Name;

        PaymentStart = transaction.PaymentStart;
        PaymentEnd = transaction.PaymentEnd;
    }
}

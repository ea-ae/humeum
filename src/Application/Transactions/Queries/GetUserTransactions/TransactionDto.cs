using Domain.Entities;

namespace Application.Transactions.Queries.GetUserTransactions;

public class TransactionDto {
    public int Id { get; set; }
    public decimal Amount { get; set; }
    
    public int? timesPerUnit { get; set; }
    public string? unit { get; set; }
    
    public DateTime PaymentStart { get; set; }
    public DateTime? PaymentEnd { get; set; }

    public TransactionDto(Transaction transaction) { // temporary; until we add AutoMapper
        Id = transaction.Id;
        Amount = transaction.Amount;

        timesPerUnit = transaction.Frequency?.TimesPerUnit;
        unit = transaction.Frequency?.Unit.Name;

        PaymentStart = transaction.PaymentStart;
        PaymentEnd = transaction.PaymentEnd;
    }
}

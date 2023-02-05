using Domain.Entities;

namespace Application.Transactions.Queries.GetUserTransactions;

public class TransactionDto {
    public int Id { get; set; }
    public decimal Amount { get; set; }
    
    public string? Unit { get; set; }
    public int? TimesPerUnit { get; set; }
    
    public DateTime PaymentStart { get; set; }
    public DateTime? PaymentEnd { get; set; }
}

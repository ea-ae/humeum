using Domain.Common;

namespace Domain.Entities;

public class TransactionType : EnumerationEntity {
    public static TransactionType Income = new("Income");
    public static TransactionType Expense = new("Expense");

    private TransactionType(string name) : base(name) { }
}

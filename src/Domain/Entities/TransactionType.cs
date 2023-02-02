using Domain.Common;

namespace Domain.Entities;

public class TransactionType : EnumerationEntity {
    public static readonly TransactionType Income = new("INCOME");
    public static readonly TransactionType Expense = new("EXPENSE");

    private TransactionType(string name) : base(name) { }

    private TransactionType() { }
}

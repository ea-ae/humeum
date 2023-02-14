using Domain.Common;

namespace Domain.TransactionAggregate.ValueObjects;

public class TransactionType : Enumeration {
    public static readonly TransactionType Always = new(1, "ALWAYS", "Always");
    public static readonly TransactionType PreRetirementOnly = new(2, "PRERETIREMENTONLY", "Pre-retirement only");
    public static readonly TransactionType RetirementOnly = new(3, "RETIREMENTONLY", "Retirement only");

    private TransactionType(int id, string code, string name) : base(id, code, name) { }

    private TransactionType() { }
}

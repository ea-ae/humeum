using Domain.Common;

namespace Domain.Entities;

public class TransactionType : Entity {
    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;

    public TransactionType(string name, string code) {
        Name = name;
        Code = code;
        // todo: validation and enum class
    }

    private TransactionType() { }
}

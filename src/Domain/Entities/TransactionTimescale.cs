using Domain.Common;

namespace Domain.Entities;

public class TransactionTimescale : Entity {
    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;

    public TransactionTimescale(string code) {
        Name = code[..1].ToUpper() + code[1..].ToLower();
        Code = code.ToUpper();
    }
    
    private TransactionTimescale() { }
}

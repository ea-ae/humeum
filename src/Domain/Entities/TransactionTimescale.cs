using Domain.Common;

namespace Domain.Entities;

public class TransactionTimescale : Entity {
    public required string Name { get; set; }
    public required string Code { get; set; }
}

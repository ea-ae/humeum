namespace Domain.Common;

public abstract class TimestampedEntity : Entity {
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}

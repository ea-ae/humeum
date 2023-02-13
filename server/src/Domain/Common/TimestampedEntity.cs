namespace Domain.Common;

public abstract class TimestampedEntity : Entity {
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; private set; } = null;

    public void UpdateModificationTimestamp() {
        ModifiedAt = DateTime.UtcNow;
    }

    public void UpdateDeletionTimestamp() {
        ModifiedAt = DateTime.UtcNow;
        DeletedAt = DateTime.UtcNow;
    }
}

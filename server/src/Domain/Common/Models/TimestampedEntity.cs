using Domain.Common.Exceptions;

namespace Domain.Common.Models;

public abstract class TimestampedEntity : Entity {
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; private set; } = null;

    public void UpdateModificationTimestamp() {
        if (DeletedAt is not null) {
            throw new DomainException(new InvalidOperationException("Cannot update deleted entity."));
        }

        ModifiedAt = DateTime.UtcNow;
    }

    public void SetDeletionTimestamp() {
        if (DeletedAt is not null) {
            throw new DomainException(new InvalidOperationException("Entity has alrady been deleted."));
        }

        ModifiedAt = DateTime.UtcNow;
        DeletedAt = DateTime.UtcNow;
    }
}

namespace Domain.Common.Interfaces;

public interface IAggregateRoot {
    bool CanBeSaved { get; }
    bool CanBeDeleted { get; }
}

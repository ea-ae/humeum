using Domain.ProfileAggregate;

namespace Domain.Common.Interfaces;

/// <summary>
/// Entities that are or can be related to profiles, aka created and owned by them.
/// </summary>
public interface IProfileEntity {
    int Id { get; }

    DateTime CreatedAt { get; }
    DateTime ModifiedAt { get; }
    DateTime? DeletedAt { get; }
}

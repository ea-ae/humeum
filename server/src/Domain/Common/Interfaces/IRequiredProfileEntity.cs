using Domain.V1.ProfileAggregate;

namespace Domain.Common.Interfaces;

/// <summary>
/// Entities that must be owned by profiles.
/// </summary>
public interface IRequiredProfileEntity : IProfileEntity {
    int ProfileId { get; }
    Profile Profile { get; }
}

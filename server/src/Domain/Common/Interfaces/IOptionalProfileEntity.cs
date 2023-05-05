using Domain.V1.ProfileAggregate;

namespace Domain.Common.Interfaces;

/// <summary>
/// Entities that can be owned by profiles.
/// </summary>
public interface IOptionalProfileEntity : IProfileEntity {
    int? ProfileId { get; }
    Profile? Profile { get; }
}

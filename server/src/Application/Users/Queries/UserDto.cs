namespace Application.Users.Queries;

public record UserDto {
    public record BriefProfile {
        public required int Id { get; init; }
        public required string Name { get; init; }
    }

    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string DisplayName { get; init; }

    public required IEnumerable<BriefProfile> Profiles { get; init; }
}

namespace Application.Profiles.Queries.GetUserProfileDetails;

public record ProfileDto {
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required decimal WithdrawalRate { get; init; }
}

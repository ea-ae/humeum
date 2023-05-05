namespace Application.V1.Assets.Queries;

public record AssetDto
{
    public record AssetTypeDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string Code { get; init; }
    }

    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal ReturnRate { get; init; }
    public required decimal StandardDeviation { get; init; }
    public required AssetTypeDto Type { get; init; }

    public required bool Default { get; init; }
}

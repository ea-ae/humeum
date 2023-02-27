namespace Application.Assets.Queries;

public record AssetTypeDto {
    public required int Id { get; init; }
    public required string Name { get; init; }
}

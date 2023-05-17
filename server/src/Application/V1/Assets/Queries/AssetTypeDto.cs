namespace Application.V1.Assets.Queries;

public record AssetTypeDto {
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Code { get; init; }
}

namespace Application.TransactionCategories.Queries;

public record CategoryDto {
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required bool Default { get; init; }
}

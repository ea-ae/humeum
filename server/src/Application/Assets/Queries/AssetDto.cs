﻿namespace Application.Assets.Queries;

public record AssetDto {
    public record AssetTypeDto {
        public required int Id { get; init; }
        public required string Name { get; init; }
    }

    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal ReturnRate { get; init; }
    public required decimal StandardDeviation { get; init; }
    public required AssetTypeDto Type { get; init; } // todo stick this class in here

    public required bool Default { get; init; }
}

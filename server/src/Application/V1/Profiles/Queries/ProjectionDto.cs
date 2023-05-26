namespace Application.V1.Profiles.Queries;

public class ProjectionDto {
    public class TimePointDto {
        public required DateOnly Date { get; init; }
        public required double LiquidWorth { get; init; }
        public required double AssetWorth { get; init; }
    }

    public required DateOnly? RetiringAt { get; init; }
    public required List<TimePointDto> TimeSeries { get; init; }
}

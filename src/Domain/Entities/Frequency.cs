namespace Domain.Entities;

public class Frequency {
    public TimeUnit Unit { get; private set; } = null!;

    int _timesPerUnit;
    public int TimesPerUnit {
        get => _timesPerUnit;
        private set {
            if (value <= 0) {
                throw new ArgumentException("Time per unit must be greater than zero.");
            }
            _timesPerUnit = value;
        } 
    }

    public Frequency(TimeUnit unit, int timesPerUnit) {
        Unit = unit;
        TimesPerUnit = timesPerUnit;
    }

    private Frequency() { }
}

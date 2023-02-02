namespace Domain.Entities;

public class TransactionFrequency {
    public TransactionTimescale Unit { get; private set; } = null!;

    private int _timesPerUnit;
    public int TimesPerUnit {
        get => _timesPerUnit;
        private set {
            if (value <= 0) {
                throw new ArgumentException("Time per unit must be greater than zero.");
            }
            _timesPerUnit = value;
        } 
    }

    public TransactionFrequency(TransactionTimescale unit, int timesPerUnit) {
        Unit = unit;
        TimesPerUnit = timesPerUnit;
    }

    private TransactionFrequency() { }
}

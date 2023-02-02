namespace Domain.Entities;

public class TransactionFrequency {
    public TransactionTimescale Unit { get; private set; } = null!;
    public int TimesPerUnit { get; private set; }

    private TransactionFrequency() { }

    public TransactionFrequency(TransactionTimescale unit, int timesPerUnit) {
        Unit = unit;
        TimesPerUnit = timesPerUnit;
    }
}

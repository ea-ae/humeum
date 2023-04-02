namespace Domain.Common.Interfaces;

public interface IBaseException {
    public int StatusCode { get; }
    public string Title { get; }
    public string Message { get; }
}

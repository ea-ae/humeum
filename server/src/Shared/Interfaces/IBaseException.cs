namespace Shared.Interfaces;

/// <summary>
/// Base exception interface that all custom exceptions in the solution implement.
/// </summary>
public interface IBaseException {
    /// <summary>HTTP status code of the exception.</summary>
    public int StatusCode { get; }
    /// <summary>Short title of the exception indicating its type.</summary>
    public string Title { get; }
    /// <summary>Detailed message of what caused the exception.</summary>
    public string Message { get; }
}

namespace Shared.Models;

/// <summary>
/// Indicates a lack of value; to be used in Results that contain either nothing or errors.
/// </summary>
public readonly struct None : IEquatable<None> {
    /// <summary>Singleton value for None.</summary>
    public static None Value { get; } = new();

    /// <summary>Compares equality between singletons.</summary>
    /// <returns>Always returns true.</returns>
    public bool Equals(None other) => true;

    /// <summary>Compares equality between singletons.</summary>
    /// <returns>Always returns true.</returns>
    public override bool Equals(object? obj) => obj is None;

    /// <summary>Compares equality between singletons.</summary>
    /// <returns>Always returns true.</returns>
    public static bool operator ==(None _1, None _2) => true;

    /// <summary>Compares inequality between singletons.</summary>
    /// <returns>Always returns false.</returns>
    public static bool operator !=(None _1, None _2) => false;

    /// <summary>Gets the hash code of the singleton.</summary>
    /// <returns>Always returns zero.</returns>
    public override int GetHashCode() => 0;
}

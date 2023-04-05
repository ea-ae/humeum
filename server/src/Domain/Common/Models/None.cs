using System.Diagnostics.CodeAnalysis;

namespace Domain.Common.Models;

/// <summary>
/// Indicates a lack of value; to be used in Results that contain either nothing or errors.
/// </summary>
public readonly struct None : IEquatable<None> {
    public static None Value { get; } = new();

    public bool Equals(None other) => true;

    public override bool Equals(object? obj) => obj is None;

    public static bool operator ==(None _1, None _2) => true;

    public static bool operator !=(None _1, None _2) => false;

    public override int GetHashCode() => 0;
}

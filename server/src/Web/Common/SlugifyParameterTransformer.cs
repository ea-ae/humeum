using System.Text.RegularExpressions;

namespace Web.Common;

/// <summary>
/// Transforms URLs to lowercase slug format automatically.
/// </summary>
public class SlugifyParameterTransformer : IOutboundParameterTransformer {
    /// <summary>
    /// Called on action URLs.
    /// </summary>
    /// <param name="value">The URL.</param>
    /// <returns>Slugified URL.</returns>
    public string? TransformOutbound(object? value) {
        if (value == null) { return null; }

        return Regex.Replace(value.ToString()!,
                             "([a-z])([A-Z])",
                             "$1-$2",
                             RegexOptions.CultureInvariant,
                             TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
    }
}

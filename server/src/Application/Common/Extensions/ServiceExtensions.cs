using Application.Common.Exceptions;

namespace Application.Common.Extensions;

/// <summary>
/// Extensions for command and query handlers.
/// </summary>
internal static class ServiceExtensions {
    /// <summary>
    /// Asserts that a set of fields is either provided fully or not at all (aka every field is null).
    /// </summary>
    /// <param name="fields">Set of fields that must be either provided fully or not at all.</param>
    /// <returns>True if the fields were fully provided, false if none were provided.</returns>
    /// <exception cref="ApplicationValidationException">If the fields were provided partially and the assertion failed.</exception>
    public static bool AssertOptionalFieldSetValidity(this List<object?> fields) {
        int optionalFieldsProvided = fields.Count(field => field is not null);
        bool allFieldsProvided = optionalFieldsProvided == fields.Count;

        if (!allFieldsProvided && optionalFieldsProvided > 0) {
            throw new ApplicationValidationException("Set of optional fields was only partially specified.");
        }

        return allFieldsProvided;
    }
}

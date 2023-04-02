using System.Text.RegularExpressions;

namespace Application.Common.Exceptions;

/// <summary>
/// This application exception signifies that an entity with given constraints couldn't be found. For example a profile owned by
/// some user, or a transaction made by a profile. The entity could technically still exist, just not by the user/profile/etc specified
/// or as a soft-deleted entity.
/// </summary>
public class NotFoundValidationException : ApplicationValidationException {
    public NotFoundValidationException(string message) : base(message) { }

    //public NotFoundValidationException(Type missingValueType) : base($"Requested {missingValueType.Name.ToLower()} does not exist.") { }
    public NotFoundValidationException(Type missingValueType)
        : base($"Requested {Regex.Replace(missingValueType.Name, "([A-Z][a-z]+)([A-Z][a-z]+)", "$1 $2").ToLower()} does not exist.") { }

    public NotFoundValidationException(string message, Type missingValueType) : base($"{message}: {missingValueType.Name}.") { }

    public override string Title => "Not Found";

    public override int StatusCode => 404;
}

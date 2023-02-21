using Domain.Common;
using Domain.Common.Exceptions;

namespace Domain.UserAggregate.ValueObjects;

public class Username : ValueObject {
    public const int MIN_USERNAME_LENGTH = 4;
    public const int MAX_USERNAME_LENGTH = 20;

    string _value = null!;
    public string Value {
        get => _value;
        private set {
            if (value.Length > MAX_USERNAME_LENGTH) {
                throw new DomainException(new ArgumentException($"Username is too long (>{MAX_USERNAME_LENGTH} characters)."));
            }

            if (value.Length < MIN_USERNAME_LENGTH) {
                throw new DomainException(new ArgumentException($"Username is too short (<{MIN_USERNAME_LENGTH} characters)."));
            }

            if (!value.All(char.IsAsciiLetterOrDigit)) {
                throw new DomainException(new ArgumentException($"Username must be ASCII-alphanumeric."));
            }

            if (!char.IsAsciiLetter(value.First())) {
                throw new DomainException(new ArgumentException($"Username must start with a letter, not a digit."));
            }

            _value = value;
        }
    }

    public Username(string value) {
        Value = value;
    }

    private Username() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return Value;
    }
}

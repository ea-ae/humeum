using System.Reflection;

namespace Domain.Common;

public abstract class EnumerationEntity : Entity, IComparable {
    public string Name { get; private set; }
    public string Code { get; private set; }

    public override string ToString() => Name;

    protected EnumerationEntity(string name, string code) => (Name, Code) = (name, code);

    protected EnumerationEntity(string code) => (Name, Code) = (code[..1].ToUpper() + code[1..].ToLower(), code);

    public static IEnumerable<T> GetAll<T>() where T : EnumerationEntity =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

    public override bool Equals(object? obj) {
        if (obj is not EnumerationEntity otherValue) {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Code.Equals(otherValue.Code);

        return typeMatches && valueMatches;
    }

    public int CompareTo(object? obj) => Code.CompareTo(((EnumerationEntity)obj!).Code);

    public override int GetHashCode() => Code.GetHashCode();
}

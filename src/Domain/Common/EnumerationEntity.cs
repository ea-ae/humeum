using System.Reflection;

using Domain.Entities;

namespace Domain.Common;

public abstract class Enumeration : ValueObject, IComparable {
    public int Id { get; protected set; }
    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;

    public override string ToString() => Name;

    protected Enumeration(int id, string code, string name) => (Id, Code, Name) = (id, code, name);

    protected Enumeration(int id, string code) : this(code) => Id = id;

    protected Enumeration(string code, string name) => (Code, Name) = (code, name);

    protected Enumeration(string code) => (Code, Name) = (code, code[..1].ToUpper() + code[1..].ToLower());

    protected Enumeration() { }

    public static T GetByCode<T>(string code) where T : Enumeration {
        return GetAll<T>().First(t => t.Code == code);
    }

    public override bool Equals(object? obj) {
        if (obj is not Enumeration otherValue) {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Code.Equals(otherValue.Code) && Name.Equals(otherValue.Name);

        return typeMatches && valueMatches;
    }
    
    public int CompareTo(object? obj) => Code.CompareTo(((Enumeration)obj!).Code);

    public override int GetHashCode() => Code.GetHashCode();

    protected static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return Code;
        yield return Name;
    }
}

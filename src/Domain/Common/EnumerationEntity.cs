using System.Reflection;

using Domain.Entities;

namespace Domain.Common;

public abstract class EnumerationEntity : Entity, IComparable {
    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;

    public override string ToString() => Name;

    protected EnumerationEntity(int id, string code, string name) => (Id, Code, Name) = (id, code, name);

    protected EnumerationEntity(int id, string code) : this(code) => Id = id;

    protected EnumerationEntity(string code, string name) => (Code, Name) = (code, name);

    protected EnumerationEntity(string code) => (Code, Name) = (code, code[..1].ToUpper() + code[1..].ToLower());

    protected EnumerationEntity() { }

    public static T GetByCode<T>(string code) where T : EnumerationEntity {
        return GetAll<T>().First(t => t.Code == code);
    }

    public override bool Equals(object? obj) {
        if (obj is not EnumerationEntity otherValue) {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Code.Equals(otherValue.Code) && Name.Equals(otherValue.Name);

        return typeMatches && valueMatches;
    }
    
    public int CompareTo(object? obj) => Code.CompareTo(((EnumerationEntity)obj!).Code);

    public override int GetHashCode() => Code.GetHashCode();

    protected static IEnumerable<T> GetAll<T>() where T : EnumerationEntity =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();
}

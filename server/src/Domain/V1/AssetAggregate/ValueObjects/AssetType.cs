using Domain.Common.Models;

namespace Domain.V1.AssetAggregate.ValueObjects;

/// <summary>
/// Types of assets available.
/// 
/// An alternative categorization would involve equities, bonds, derivatives, and hybrids. This type of
/// categorization is however not used here for simplicity's sake.
/// </summary>
public class AssetType : Enumeration
{
    public static readonly AssetType Liquid = new(1, "LIQUID", "Liquid/Cash");
    public static readonly AssetType Index = new(2, "INDEX", "Index fund");
    public static readonly AssetType Managed = new(3, "MANAGED", "Managed fund");
    public static readonly AssetType RealEstate = new(4, "REALESTATE", "Real estate");
    public static readonly AssetType Bond = new(5, "BOND", "Bond");
    public static readonly AssetType Stock = new(6, "STOCK", "Stock/Derivative");
    public static readonly AssetType Other = new(7, "OTHER", "Other");

    private AssetType(int id, string code, string name) : base(id, code, name) { }

    private AssetType() { }

    public static IEnumerable<AssetType> GetAll() => GetAll<AssetType>();
}

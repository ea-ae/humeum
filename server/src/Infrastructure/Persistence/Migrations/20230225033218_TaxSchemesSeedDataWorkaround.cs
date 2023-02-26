using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class TaxSchemesSeedDataWorkaround : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5430), new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5432) });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5442), new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5442) });

        migrationBuilder.InsertData(
            table: "TaxSchemes",
            columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "ModifiedAt", "Name", "TaxRate", "IncentiveScheme_MaxApplicableIncome", "IncentiveScheme_MaxIncomePercentage", "IncentiveScheme_MinAge", "IncentiveScheme_TaxRefundRate" },
            values: new object[,]
            {
                { 1, new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5468), null, "Regular flat income tax in Estonia, applicable to all income by default. First 654EUR/mo aka 7848EUR/yr are tax-free.", new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5469), "Income tax", 20m, 7848, null, null, 20m },
                { 2, new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5471), null, "Asset income invested through III pillar, with an account opened in 2021 or later. Term pensions based on life expectancy, not included here, provide a 20% discount.", new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5471), "III pillar, post-2021", 20m, 6000, 15m, 60, 10m },
                { 3, new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5472), null, "Asset income invested through III pillar, with an account opened before 2021. Term pensions based on life expectancy, not included here, provide a 20% discount.", new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5472), "III pillar, pre-2021", 20m, 6000, 15m, 55, 10m }
            });

        migrationBuilder.InsertData(
            table: "TaxSchemes",
            columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "ModifiedAt", "Name", "TaxRate" },
            values: new object[] { 4, new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5533), null, "Income that due to special circumstances (e.g. charity) is not taxed whatsoever.", new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5533), "Non-taxable income", 0m });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DeleteData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 3, 15, 39, 140, DateTimeKind.Utc).AddTicks(3271), new DateTime(2023, 2, 25, 3, 15, 39, 140, DateTimeKind.Utc).AddTicks(3275) });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 3, 15, 39, 140, DateTimeKind.Utc).AddTicks(3285), new DateTime(2023, 2, 25, 3, 15, 39, 140, DateTimeKind.Utc).AddTicks(3286) });
    }
}

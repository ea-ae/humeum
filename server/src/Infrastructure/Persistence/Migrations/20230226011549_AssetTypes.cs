using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AssetTypes : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.AddColumn<int>(
            name: "TypeId",
            table: "Assets",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "AssetTypes",
            columns: table => new {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Code = table.Column<string>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_AssetTypes", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "AssetTypes",
            columns: new[] { "Id", "Code", "Name" },
            values: new object[,]
            {
                { 1, "LIQUID", "Liquid/Cash" },
                { 2, "INDEX", "Index fund" },
                { 3, "MANAGED", "Managed fund" },
                { 4, "REALESTATE", "Real estate" },
                { 5, "BOND", "Bond" },
                { 6, "STOCK", "Stock/Derivative" },
                { 7, "OTHER", "Other" }
            });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt", "TypeId" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3746), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3747), 2 });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt", "TypeId" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3758), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3758), 5 });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3812), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3812) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3815), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3815) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3816), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3816) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3817), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(3817) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2210), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2213) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2216), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2216) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2217), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2217) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2218), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2219) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 5,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2220), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2220) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 6,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2221), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2221) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 7,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2222), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2222) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 8,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2234), new DateTime(2023, 2, 26, 1, 15, 48, 903, DateTimeKind.Utc).AddTicks(2235) });

        migrationBuilder.CreateIndex(
            name: "IX_Assets_TypeId",
            table: "Assets",
            column: "TypeId");

        migrationBuilder.AddForeignKey(
            name: "FK_Assets_AssetTypes_TypeId",
            table: "Assets",
            column: "TypeId",
            principalTable: "AssetTypes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropForeignKey(
            name: "FK_Assets_AssetTypes_TypeId",
            table: "Assets");

        migrationBuilder.DropTable(
            name: "AssetTypes");

        migrationBuilder.DropIndex(
            name: "IX_Assets_TypeId",
            table: "Assets");

        migrationBuilder.DropColumn(
            name: "TypeId",
            table: "Assets");

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4675), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4676) });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4685), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4686) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4717), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4717) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4720), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4720) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4721), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4722) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4722), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4723) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3595), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3596) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3599), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3599) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3600), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3600) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3601), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3601) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 5,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3602), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3603) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 6,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3604), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3604) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 7,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3606), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3606) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 8,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3615), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3615) });
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class ProfileAssets : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 23, 13, 39, 34, 587, DateTimeKind.Utc).AddTicks(9032), new DateTime(2023, 2, 23, 13, 39, 34, 587, DateTimeKind.Utc).AddTicks(9034) });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 23, 13, 39, 34, 587, DateTimeKind.Utc).AddTicks(9045), new DateTime(2023, 2, 23, 13, 39, 34, 587, DateTimeKind.Utc).AddTicks(9045) });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 21, 8, 47, 54, 601, DateTimeKind.Utc).AddTicks(6963), new DateTime(2023, 2, 21, 8, 47, 54, 601, DateTimeKind.Utc).AddTicks(6966) });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 21, 8, 47, 54, 601, DateTimeKind.Utc).AddTicks(6978), new DateTime(2023, 2, 21, 8, 47, 54, 601, DateTimeKind.Utc).AddTicks(6979) });
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Typo : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.UpdateData(
            table: "TransactionTypes",
            keyColumn: "Id",
            keyValue: 2,
            column: "Code",
            value: "PRERETIREMENTONLY");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.UpdateData(
            table: "TransactionTypes",
            keyColumn: "Id",
            keyValue: 2,
            column: "Code",
            value: "PRERETIREMENTOnly");
    }
}

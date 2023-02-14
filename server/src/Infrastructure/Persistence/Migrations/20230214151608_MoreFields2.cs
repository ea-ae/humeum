using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class MoreFields2 : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "Transactions",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "Transactions",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "Profiles",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "Profiles",
            type: "TEXT",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<decimal>(
            name: "WithdrawalRate",
            table: "Profiles",
            type: "TEXT",
            nullable: false,
            defaultValue: 0m);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropColumn(
            name: "Description",
            table: "Transactions");

        migrationBuilder.DropColumn(
            name: "Name",
            table: "Transactions");

        migrationBuilder.DropColumn(
            name: "Description",
            table: "Profiles");

        migrationBuilder.DropColumn(
            name: "Name",
            table: "Profiles");

        migrationBuilder.DropColumn(
            name: "WithdrawalRate",
            table: "Profiles");
    }
}

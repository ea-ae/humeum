using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TaxSchemesWithIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Profiles_ProfileId",
                table: "Assets");

            migrationBuilder.AddColumn<int>(
                name: "AssetId",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxSchemeId",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TaxSchemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    TaxRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncentiveSchemeTaxRefundRate = table.Column<decimal>(name: "IncentiveScheme_TaxRefundRate", type: "TEXT", nullable: true),
                    IncentiveSchemeMinAge = table.Column<int>(name: "IncentiveScheme_MinAge", type: "INTEGER", nullable: true),
                    IncentiveSchemeMaxIncomePercentage = table.Column<decimal>(name: "IncentiveScheme_MaxIncomePercentage", type: "TEXT", nullable: true),
                    IncentiveSchemeMaxApplicableIncome = table.Column<int>(name: "IncentiveScheme_MaxApplicableIncome", type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxSchemes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 2, 25, 3, 13, 47, 38, DateTimeKind.Utc).AddTicks(3137), new DateTime(2023, 2, 25, 3, 13, 47, 38, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 2, 25, 3, 13, 47, 38, DateTimeKind.Utc).AddTicks(3149), new DateTime(2023, 2, 25, 3, 13, 47, 38, DateTimeKind.Utc).AddTicks(3149) });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AssetId",
                table: "Transactions",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TaxSchemeId",
                table: "Transactions",
                column: "TaxSchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Profiles_ProfileId",
                table: "Assets",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Assets_AssetId",
                table: "Transactions",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TaxSchemes_TaxSchemeId",
                table: "Transactions",
                column: "TaxSchemeId",
                principalTable: "TaxSchemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Profiles_ProfileId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_AssetId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TaxSchemes_TaxSchemeId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TaxSchemes");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AssetId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TaxSchemeId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TaxSchemeId",
                table: "Transactions");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Profiles_ProfileId",
                table: "Assets",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5724), new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5724) });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5765), new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5765) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5825), new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5825) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5827), new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5828) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5829), new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5829) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5830), new DateTime(2023, 4, 12, 13, 9, 52, 96, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9630), new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9631) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9634), new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9635) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9635), new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9636) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9637), new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9637) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9638), new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9638) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9639), new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9639) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9640), new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9640) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9641), new DateTime(2023, 4, 12, 13, 9, 52, 95, DateTimeKind.Utc).AddTicks(9641) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(2962), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(2963) });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(2972), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(2972) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(3024), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(3024) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(3027), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(3027) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(3028), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(3029) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(3030), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1702), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1707) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1709), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1709) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1710), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1710) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1711), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1711) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1713), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1713) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1714), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1714) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1715), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1715) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1764), new DateTime(2023, 3, 5, 15, 58, 11, 440, DateTimeKind.Utc).AddTicks(1765) });
        }
    }
}

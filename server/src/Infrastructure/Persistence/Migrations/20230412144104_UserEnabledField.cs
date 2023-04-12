using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserEnabledField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8198), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8200) });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8255), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8255) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8300), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8301) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8303), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8303) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8304), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8304) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8305), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(8305) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1777), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1779) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1780), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1781) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1781), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1782) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1782), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1783) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1783), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1784) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1784), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1785) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1785), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1785) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1786), new DateTime(2023, 4, 12, 14, 41, 4, 301, DateTimeKind.Utc).AddTicks(1786) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4536), new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4539) });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4612), new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4612) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4681), new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4681) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4683), new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4684) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4684), new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4685) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4686), new DateTime(2023, 4, 12, 13, 11, 51, 668, DateTimeKind.Utc).AddTicks(4686) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7372), new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7374) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7375), new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7376) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7377), new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7377) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7378), new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7378) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7379), new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7379) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7380), new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7381), new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7381) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7382), new DateTime(2023, 4, 12, 13, 11, 51, 667, DateTimeKind.Utc).AddTicks(7382) });
        }
    }
}

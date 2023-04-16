using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1555), new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1557) });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1670), new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1740), new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1742), new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1743) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1744), new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1744) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1745), new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1745) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4453), new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4456) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4459), new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4459) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4460), new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4460) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4461), new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4462) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4463), new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4463) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4464), new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4465) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4514), new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4515) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4516), new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4516) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

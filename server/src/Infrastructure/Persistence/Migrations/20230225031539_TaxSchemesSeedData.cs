using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TaxSchemesSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}

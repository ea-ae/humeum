using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TaxIncentiveSchemeRemoveNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 537, DateTimeKind.Utc).AddTicks(173), new DateTime(2023, 5, 25, 10, 3, 50, 537, DateTimeKind.Utc).AddTicks(175) });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 537, DateTimeKind.Utc).AddTicks(220), new DateTime(2023, 5, 25, 10, 3, 50, 537, DateTimeKind.Utc).AddTicks(221) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 177, DateTimeKind.Utc).AddTicks(3636), new DateTime(2023, 5, 25, 10, 3, 50, 177, DateTimeKind.Utc).AddTicks(3637) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 178, DateTimeKind.Utc).AddTicks(408), new DateTime(2023, 5, 25, 10, 3, 50, 178, DateTimeKind.Utc).AddTicks(409) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 178, DateTimeKind.Utc).AddTicks(425), new DateTime(2023, 5, 25, 10, 3, 50, 178, DateTimeKind.Utc).AddTicks(425) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 178, DateTimeKind.Utc).AddTicks(428), new DateTime(2023, 5, 25, 10, 3, 50, 178, DateTimeKind.Utc).AddTicks(428) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3943), new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3945) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3947), new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3947) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3947), new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3948) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3948), new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3949), new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3950) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3950), new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3951) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3951), new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3952) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3952), new DateTime(2023, 5, 25, 10, 3, 50, 536, DateTimeKind.Utc).AddTicks(3953) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 891, DateTimeKind.Utc).AddTicks(2255), new DateTime(2023, 5, 25, 10, 2, 9, 891, DateTimeKind.Utc).AddTicks(2257) });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 891, DateTimeKind.Utc).AddTicks(2308), new DateTime(2023, 5, 25, 10, 2, 9, 891, DateTimeKind.Utc).AddTicks(2309) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 440, DateTimeKind.Utc).AddTicks(566), new DateTime(2023, 5, 25, 10, 2, 9, 440, DateTimeKind.Utc).AddTicks(570) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1190), new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1195) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1224), new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1224) });

            migrationBuilder.UpdateData(
                table: "TaxSchemes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1229), new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4416), new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4417) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4419), new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4419) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4420), new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4420) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4421), new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4422) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4422), new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4423) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4423), new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4424) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4425), new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4425) });

            migrationBuilder.UpdateData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4426), new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4426) });
        }
    }
}

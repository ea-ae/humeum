using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class FixField : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
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
}

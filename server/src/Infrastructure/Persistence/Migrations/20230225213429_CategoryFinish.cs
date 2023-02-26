using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class CategoryFinish : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.AddColumn<int>(
            name: "ProfileId",
            table: "TransactionCategories",
            type: "INTEGER",
            nullable: true);

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6540), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6541) });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6555), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6556) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6610), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6610) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6613), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6613) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6614), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6615) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6616), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(6616) });

        migrationBuilder.InsertData(
            table: "TransactionCategories",
            columns: new[] { "Id", "CreatedAt", "DeletedAt", "ModifiedAt", "Name", "ProfileId" },
            values: new object[,]
            {
                { 1, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5389), null, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5392), "General", null },
                { 2, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5394), null, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5394), "Investing", null },
                { 3, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5395), null, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5395), "Work, Education, & Business", null },
                { 4, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5396), null, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5396), "Recreation & Lifestyle", null },
                { 5, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5397), null, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5397), "Food & Clothing", null },
                { 6, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5398), null, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5398), "Housing & Utilities", null },
                { 7, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5399), null, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5399), "Transportation", null },
                { 8, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5400), null, new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5401), "Gifts & Donations", null }
            });

        migrationBuilder.CreateIndex(
            name: "IX_TransactionCategories_ProfileId",
            table: "TransactionCategories",
            column: "ProfileId");

        migrationBuilder.AddForeignKey(
            name: "FK_TransactionCategories_Profiles_ProfileId",
            table: "TransactionCategories",
            column: "ProfileId",
            principalTable: "Profiles",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropForeignKey(
            name: "FK_TransactionCategories_Profiles_ProfileId",
            table: "TransactionCategories");

        migrationBuilder.DropIndex(
            name: "IX_TransactionCategories_ProfileId",
            table: "TransactionCategories");

        migrationBuilder.DeleteData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 8);

        migrationBuilder.DropColumn(
            name: "ProfileId",
            table: "TransactionCategories");

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9559), new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9561) });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9571), new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9572) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9607), new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9607) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9609), new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9610) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9610), new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9611) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9612), new DateTime(2023, 2, 25, 21, 7, 32, 116, DateTimeKind.Utc).AddTicks(9612) });
    }
}

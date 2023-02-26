using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class TransactionCategories : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "TransactionCategories",
            columns: table => new {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_TransactionCategories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "TransactionTransactionCategory",
            columns: table => new {
                CategoriesId = table.Column<int>(type: "INTEGER", nullable: false),
                TransactionsId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_TransactionTransactionCategory", x => new { x.CategoriesId, x.TransactionsId });
                table.ForeignKey(
                    name: "FK_TransactionTransactionCategory_TransactionCategories_CategoriesId",
                    column: x => x.CategoriesId,
                    principalTable: "TransactionCategories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_TransactionTransactionCategory_Transactions_TransactionsId",
                    column: x => x.TransactionsId,
                    principalTable: "Transactions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

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

        migrationBuilder.CreateIndex(
            name: "IX_TransactionTransactionCategory_TransactionsId",
            table: "TransactionTransactionCategory",
            column: "TransactionsId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "TransactionTransactionCategory");

        migrationBuilder.DropTable(
            name: "TransactionCategories");

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5430), new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5432) });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5442), new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5442) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5468), new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5469) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5471), new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5471) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5472), new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5472) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5533), new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5533) });
    }
}

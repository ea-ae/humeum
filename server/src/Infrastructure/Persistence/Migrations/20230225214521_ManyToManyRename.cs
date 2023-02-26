using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class ManyToManyRename : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropForeignKey(
            name: "FK_TransactionTransactionCategory_TransactionCategories_CategoriesId",
            table: "TransactionTransactionCategory");

        migrationBuilder.DropForeignKey(
            name: "FK_TransactionTransactionCategory_Transactions_TransactionsId",
            table: "TransactionTransactionCategory");

        migrationBuilder.DropPrimaryKey(
            name: "PK_TransactionTransactionCategory",
            table: "TransactionTransactionCategory");

        migrationBuilder.RenameTable(
            name: "TransactionTransactionCategory",
            newName: "TransactionWithCategory");

        migrationBuilder.RenameIndex(
            name: "IX_TransactionTransactionCategory_TransactionsId",
            table: "TransactionWithCategory",
            newName: "IX_TransactionWithCategory_TransactionsId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_TransactionWithCategory",
            table: "TransactionWithCategory",
            columns: new[] { "CategoriesId", "TransactionsId" });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4675), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4676) });

        migrationBuilder.UpdateData(
            table: "Assets",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4685), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4686) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4717), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4717) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4720), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4720) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4721), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4722) });

        migrationBuilder.UpdateData(
            table: "TaxSchemes",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4722), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(4723) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3595), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3596) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3599), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3599) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3600), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3600) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3601), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3601) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 5,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3602), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3603) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 6,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3604), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3604) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 7,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3606), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3606) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 8,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3615), new DateTime(2023, 2, 25, 21, 45, 20, 997, DateTimeKind.Utc).AddTicks(3615) });

        migrationBuilder.AddForeignKey(
            name: "FK_TransactionWithCategory_TransactionCategories_CategoriesId",
            table: "TransactionWithCategory",
            column: "CategoriesId",
            principalTable: "TransactionCategories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_TransactionWithCategory_Transactions_TransactionsId",
            table: "TransactionWithCategory",
            column: "TransactionsId",
            principalTable: "Transactions",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropForeignKey(
            name: "FK_TransactionWithCategory_TransactionCategories_CategoriesId",
            table: "TransactionWithCategory");

        migrationBuilder.DropForeignKey(
            name: "FK_TransactionWithCategory_Transactions_TransactionsId",
            table: "TransactionWithCategory");

        migrationBuilder.DropPrimaryKey(
            name: "PK_TransactionWithCategory",
            table: "TransactionWithCategory");

        migrationBuilder.RenameTable(
            name: "TransactionWithCategory",
            newName: "TransactionTransactionCategory");

        migrationBuilder.RenameIndex(
            name: "IX_TransactionWithCategory_TransactionsId",
            table: "TransactionTransactionCategory",
            newName: "IX_TransactionTransactionCategory_TransactionsId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_TransactionTransactionCategory",
            table: "TransactionTransactionCategory",
            columns: new[] { "CategoriesId", "TransactionsId" });

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

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5389), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5392) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5394), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5394) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5395), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5395) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5396), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5396) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 5,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5397), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5397) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 6,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5398), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5398) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 7,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5399), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5399) });

        migrationBuilder.UpdateData(
            table: "TransactionCategories",
            keyColumn: "Id",
            keyValue: 8,
            columns: new[] { "CreatedAt", "ModifiedAt" },
            values: new object[] { new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5400), new DateTime(2023, 2, 25, 21, 34, 28, 684, DateTimeKind.Utc).AddTicks(5401) });

        migrationBuilder.AddForeignKey(
            name: "FK_TransactionTransactionCategory_TransactionCategories_CategoriesId",
            table: "TransactionTransactionCategory",
            column: "CategoriesId",
            principalTable: "TransactionCategories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_TransactionTransactionCategory_Transactions_TransactionsId",
            table: "TransactionTransactionCategory",
            column: "TransactionsId",
            principalTable: "Transactions",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Transactions : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "TransactionTimescales",
            columns: table => new {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Code = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_TransactionTimescales", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "TransactionTypes",
            columns: table => new {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Code = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_TransactionTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Transactions",
            columns: table => new {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                End = table.Column<DateTime>(type: "TEXT", nullable: true),
                Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                TypeId = table.Column<int>(type: "INTEGER", nullable: false),
                TimescaleId = table.Column<int>(type: "INTEGER", nullable: false),
                PerTimescale = table.Column<bool>(type: "INTEGER", nullable: false),
                PaymentStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                PaymentEnd = table.Column<DateTime>(type: "TEXT", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_Transactions", x => x.Id);
                table.ForeignKey(
                    name: "FK_Transactions_TransactionTimescales_TimescaleId",
                    column: x => x.TimescaleId,
                    principalTable: "TransactionTimescales",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Transactions_TransactionTypes_TypeId",
                    column: x => x.TypeId,
                    principalTable: "TransactionTypes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_TimescaleId",
            table: "Transactions",
            column: "TimescaleId");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_TypeId",
            table: "Transactions",
            column: "TypeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "Transactions");

        migrationBuilder.DropTable(
            name: "TransactionTimescales");

        migrationBuilder.DropTable(
            name: "TransactionTypes");
    }
}

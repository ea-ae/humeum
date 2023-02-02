using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTimescales_TimescaleId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TimescaleId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PerTimescale",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TimescaleId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Transactions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Transactions",
                newName: "DeletedAt");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Frequency_TimesPerUnit",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Frequency_UnitId",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypes_Code",
                table: "TransactionTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTimescales_Code",
                table: "TransactionTimescales",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Frequency_UnitId",
                table: "Transactions",
                column: "Frequency_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTimescales_Frequency_UnitId",
                table: "Transactions",
                column: "Frequency_UnitId",
                principalTable: "TransactionTimescales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTimescales_Frequency_UnitId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionTypes_Code",
                table: "TransactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_TransactionTimescales_Code",
                table: "TransactionTimescales");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_Frequency_UnitId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Frequency_TimesPerUnit",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Frequency_UnitId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Transactions",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Transactions",
                newName: "Start");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "PerTimescale",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TimescaleId",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TimescaleId",
                table: "Transactions",
                column: "TimescaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTimescales_TimescaleId",
                table: "Transactions",
                column: "TimescaleId",
                principalTable: "TransactionTimescales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Misc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "TimeUnitId",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TimeUnitId",
                table: "Transactions",
                column: "TimeUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTimescales_TimeUnitId",
                table: "Transactions",
                column: "TimeUnitId",
                principalTable: "TransactionTimescales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTimescales_TimeUnitId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TimeUnitId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TimeUnitId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}

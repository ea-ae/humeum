using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OwnedReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTimeUnits_TimeUnitId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TimeUnitId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TimeUnitId",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Transactions_TransactionTimeUnits_TimeUnitId",
                table: "Transactions",
                column: "TimeUnitId",
                principalTable: "TransactionTimeUnits",
                principalColumn: "Id");
        }
    }
}

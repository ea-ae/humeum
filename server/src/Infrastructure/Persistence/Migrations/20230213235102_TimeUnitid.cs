using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TimeUnitid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTimeUnits_PaymentTimeline_Frequency_UnitId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "PaymentTimeline_Frequency_UnitId",
                table: "Transactions",
                newName: "PaymentTimeline_Frequency_TimeUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_PaymentTimeline_Frequency_UnitId",
                table: "Transactions",
                newName: "IX_Transactions_PaymentTimeline_Frequency_TimeUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTimeUnits_PaymentTimeline_Frequency_TimeUnitId",
                table: "Transactions",
                column: "PaymentTimeline_Frequency_TimeUnitId",
                principalTable: "TransactionTimeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTimeUnits_PaymentTimeline_Frequency_TimeUnitId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "PaymentTimeline_Frequency_TimeUnitId",
                table: "Transactions",
                newName: "PaymentTimeline_Frequency_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_PaymentTimeline_Frequency_TimeUnitId",
                table: "Transactions",
                newName: "IX_Transactions_PaymentTimeline_Frequency_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTimeUnits_PaymentTimeline_Frequency_UnitId",
                table: "Transactions",
                column: "PaymentTimeline_Frequency_UnitId",
                principalTable: "TransactionTimeUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

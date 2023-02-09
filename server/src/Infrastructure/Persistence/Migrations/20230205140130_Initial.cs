using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "TransactionTimeUnits",
            columns: table => new {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Code = table.Column<string>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_TransactionTimeUnits", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "TransactionTypes",
            columns: table => new {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Code = table.Column<string>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_TransactionTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Transactions",
            columns: table => new {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                TypeId = table.Column<int>(type: "INTEGER", nullable: false),
                PaymentTimelineFrequencyUnitId = table.Column<int>(name: "PaymentTimeline_Frequency_UnitId", type: "INTEGER", nullable: true),
                PaymentTimelineFrequencyTimesPerUnit = table.Column<int>(name: "PaymentTimeline_Frequency_TimesPerUnit", type: "INTEGER", nullable: true),
                PaymentTimelineTimePeriodStart = table.Column<DateTime>(name: "PaymentTimeline_TimePeriod_Start", type: "TEXT", nullable: false),
                PaymentTimelineTimePeriodEnd = table.Column<DateTime>(name: "PaymentTimeline_TimePeriod_End", type: "TEXT", nullable: true),
                TimeUnitId = table.Column<int>(type: "INTEGER", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_Transactions", x => x.Id);
                table.ForeignKey(
                    name: "FK_Transactions_TransactionTimeUnits_PaymentTimeline_Frequency_UnitId",
                    column: x => x.PaymentTimelineFrequencyUnitId,
                    principalTable: "TransactionTimeUnits",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Transactions_TransactionTimeUnits_TimeUnitId",
                    column: x => x.TimeUnitId,
                    principalTable: "TransactionTimeUnits",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Transactions_TransactionTypes_TypeId",
                    column: x => x.TypeId,
                    principalTable: "TransactionTypes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "TransactionTimeUnits",
            columns: new[] { "Id", "Code", "Name" },
            values: new object[,]
            {
                { 1, "HOURS", "Hours" },
                { 2, "DAYS", "Days" },
                { 3, "WEEKS", "Weeks" },
                { 4, "MONTHS", "Months" },
                { 5, "YEARS", "Years" }
            });

        migrationBuilder.InsertData(
            table: "TransactionTypes",
            columns: new[] { "Id", "Code", "Name" },
            values: new object[,]
            {
                { 1, "INCOME", "Income" },
                { 2, "EXPENSE", "Expense" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_PaymentTimeline_Frequency_UnitId",
            table: "Transactions",
            column: "PaymentTimeline_Frequency_UnitId");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_TimeUnitId",
            table: "Transactions",
            column: "TimeUnitId");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_TypeId",
            table: "Transactions",
            column: "TypeId");

        migrationBuilder.CreateIndex(
            name: "IX_TransactionTimeUnits_Code",
            table: "TransactionTimeUnits",
            column: "Code",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_TransactionTypes_Code",
            table: "TransactionTypes",
            column: "Code",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "Transactions");

        migrationBuilder.DropTable(
            name: "TransactionTimeUnits");

        migrationBuilder.DropTable(
            name: "TransactionTypes");
    }
}

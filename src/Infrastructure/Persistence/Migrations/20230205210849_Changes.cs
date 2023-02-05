using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Changes : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.RenameColumn(
            name: "PaymentTimeline_TimePeriod_Start",
            table: "Transactions",
            newName: "PaymentTimeline_Period_Start");

        migrationBuilder.RenameColumn(
            name: "PaymentTimeline_TimePeriod_End",
            table: "Transactions",
            newName: "PaymentTimeline_Period_End");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.RenameColumn(
            name: "PaymentTimeline_Period_Start",
            table: "Transactions",
            newName: "PaymentTimeline_TimePeriod_Start");

        migrationBuilder.RenameColumn(
            name: "PaymentTimeline_Period_End",
            table: "Transactions",
            newName: "PaymentTimeline_TimePeriod_End");
    }
}

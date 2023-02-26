using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AssetAggregate : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "Assets",
            columns: table => new {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Description = table.Column<string>(type: "TEXT", nullable: true),
                ReturnRate = table.Column<decimal>(type: "TEXT", nullable: false),
                StandardDeviation = table.Column<decimal>(type: "TEXT", nullable: false),
                ProfileId = table.Column<int>(type: "INTEGER", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_Assets", x => x.Id);
                table.ForeignKey(
                    name: "FK_Assets_Profiles_ProfileId",
                    column: x => x.ProfileId,
                    principalTable: "Profiles",
                    principalColumn: "Id");
            });

        migrationBuilder.InsertData(
            table: "Assets",
            columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "ModifiedAt", "Name", "ProfileId", "ReturnRate", "StandardDeviation" },
            values: new object[,]
            {
                { 1, new DateTime(2023, 2, 21, 8, 47, 54, 601, DateTimeKind.Utc).AddTicks(6963), null, "Index funds track the performance of a particular market index; great diversification, low fees, and easy management.", new DateTime(2023, 2, 21, 8, 47, 54, 601, DateTimeKind.Utc).AddTicks(6966), "Index fund (default)", null, 8.1m, 15.2m },
                { 2, new DateTime(2023, 2, 21, 8, 47, 54, 601, DateTimeKind.Utc).AddTicks(6978), null, "Bond funds provide great diversification potential and are stereotypically less volatile than other securities.", new DateTime(2023, 2, 21, 8, 47, 54, 601, DateTimeKind.Utc).AddTicks(6979), "Bond fund (default)", null, 1.9m, 3.0m }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Assets_ProfileId",
            table: "Assets",
            column: "ProfileId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "Assets");
    }
}

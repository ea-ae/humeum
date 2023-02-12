using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class NoUserAggr2 : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "User");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UsernameValue = table.Column<string>(name: "Username_Value", type: "TEXT", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                Email = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_User", x => x.Id);
            });
    }
}

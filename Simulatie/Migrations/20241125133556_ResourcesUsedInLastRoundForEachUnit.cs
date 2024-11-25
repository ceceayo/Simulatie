using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulatie.Migrations
{
    /// <inheritdoc />
    public partial class ResourcesUsedInLastRoundForEachUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResourcesUsedLastRound",
                table: "SimulatedUnits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourcesUsedLastRound",
                table: "SimulatedUnits");
        }
    }
}

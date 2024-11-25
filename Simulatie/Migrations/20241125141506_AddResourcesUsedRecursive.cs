using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulatie.Migrations
{
    /// <inheritdoc />
    public partial class AddResourcesUsedRecursive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResourcesUsedLastRoundRecursive",
                table: "SimulatedUnits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourcesUsedLastRoundRecursive",
                table: "SimulatedUnits");
        }
    }
}

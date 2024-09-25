using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulatie.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimulatedUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatedUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatedUnits_SimulatedUnits_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "SimulatedUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitArguments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    SimulatedUnitId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitArguments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitArguments_SimulatedUnits_SimulatedUnitId",
                        column: x => x.SimulatedUnitId,
                        principalTable: "SimulatedUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimulatedUnits_OwnerId",
                table: "SimulatedUnits",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitArguments_SimulatedUnitId",
                table: "UnitArguments",
                column: "SimulatedUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitArguments");

            migrationBuilder.DropTable(
                name: "SimulatedUnits");
        }
    }
}

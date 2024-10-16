using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulatie.Migrations
{
    /// <inheritdoc />
    public partial class Step1UseListForArgs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitArguments_SimulatedUnits_SimulatedUnitId",
                table: "UnitArguments");

            migrationBuilder.DropIndex(
                name: "IX_UnitArguments_SimulatedUnitId",
                table: "UnitArguments");

            migrationBuilder.DropColumn(
                name: "SimulatedUnitId",
                table: "UnitArguments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SimulatedUnitId",
                table: "UnitArguments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitArguments_SimulatedUnitId",
                table: "UnitArguments",
                column: "SimulatedUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitArguments_SimulatedUnits_SimulatedUnitId",
                table: "UnitArguments",
                column: "SimulatedUnitId",
                principalTable: "SimulatedUnits",
                principalColumn: "Id");
        }
    }
}

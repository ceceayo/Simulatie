using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulatie.Migrations
{
    /// <inheritdoc />
    public partial class Step3UseOwnerInUnitArgInsteadOfListInSimulatedUnit : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "UnitArguments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UnitArguments_OwnerId",
                table: "UnitArguments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitArguments_SimulatedUnits_OwnerId",
                table: "UnitArguments",
                column: "OwnerId",
                principalTable: "SimulatedUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitArguments_SimulatedUnits_OwnerId",
                table: "UnitArguments");

            migrationBuilder.DropIndex(
                name: "IX_UnitArguments_OwnerId",
                table: "UnitArguments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "UnitArguments");

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

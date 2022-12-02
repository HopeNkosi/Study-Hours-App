using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Hours_App.Data.Migrations
{
    public partial class editcreations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModulesDashboard_SemesterDashbaord_SemesterDashbaordId",
                table: "ModulesDashboard");

            migrationBuilder.DropColumn(
                name: "SemesterDashboardId",
                table: "ModulesDashboard");

            migrationBuilder.AlterColumn<int>(
                name: "SemesterDashbaordId",
                table: "ModulesDashboard",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulesDashboard_SemesterDashbaord_SemesterDashbaordId",
                table: "ModulesDashboard",
                column: "SemesterDashbaordId",
                principalTable: "SemesterDashbaord",
                principalColumn: "SemesterDashbaordId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModulesDashboard_SemesterDashbaord_SemesterDashbaordId",
                table: "ModulesDashboard");

            migrationBuilder.AlterColumn<int>(
                name: "SemesterDashbaordId",
                table: "ModulesDashboard",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SemesterDashboardId",
                table: "ModulesDashboard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulesDashboard_SemesterDashbaord_SemesterDashbaordId",
                table: "ModulesDashboard",
                column: "SemesterDashbaordId",
                principalTable: "SemesterDashbaord",
                principalColumn: "SemesterDashbaordId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

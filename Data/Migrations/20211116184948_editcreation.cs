using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Hours_App.Data.Migrations
{
    public partial class editcreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SemesterDashbaord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SelfStudyHours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MyHoursDashboard",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ModulesDashboard",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SemesterDashbaord");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SelfStudyHours");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MyHoursDashboard");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ModulesDashboard");
        }
    }
}

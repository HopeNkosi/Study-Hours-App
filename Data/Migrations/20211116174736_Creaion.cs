using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Hours_App.Data.Migrations
{
    public partial class Creaion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelfStudyHours",
                columns: table => new
                {
                    SelfStudyHoursId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleCredits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelfStudyHour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOfHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOfHoursLeft = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfStudyHours", x => x.SelfStudyHoursId);
                });

            migrationBuilder.CreateTable(
                name: "SemesterDashbaord",
                columns: table => new
                {
                    SemesterDashbaordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterDuration = table.Column<int>(type: "int", nullable: false),
                    SemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SemesterEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterDashbaord", x => x.SemesterDashbaordId);
                });

            migrationBuilder.CreateTable(
                name: "ModulesDashboard",
                columns: table => new
                {
                    ModulesDashboardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleNumberOfCredits = table.Column<int>(type: "int", nullable: false),
                    ClassHoursPerWeek = table.Column<int>(type: "int", nullable: false),
                    MySelfStudy = table.Column<double>(type: "float", nullable: false),
                    SemesterDashboardId = table.Column<int>(type: "int", nullable: false),
                    SemesterDashbaordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulesDashboard", x => x.ModulesDashboardId);
                    table.ForeignKey(
                        name: "FK_ModulesDashboard_SemesterDashbaord_SemesterDashbaordId",
                        column: x => x.SemesterDashbaordId,
                        principalTable: "SemesterDashbaord",
                        principalColumn: "SemesterDashbaordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MyHoursDashboard",
                columns: table => new
                {
                    MyHoursDashboardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModulesDashboardId = table.Column<int>(type: "int", nullable: false),
                    NumOfHoursSpent = table.Column<int>(type: "int", nullable: false),
                    NumOfHoursLeft = table.Column<int>(type: "int", nullable: false),
                    Dateworked = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyHoursDashboard", x => x.MyHoursDashboardId);
                    table.ForeignKey(
                        name: "FK_MyHoursDashboard_ModulesDashboard_ModulesDashboardId",
                        column: x => x.ModulesDashboardId,
                        principalTable: "ModulesDashboard",
                        principalColumn: "ModulesDashboardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModulesDashboard_SemesterDashbaordId",
                table: "ModulesDashboard",
                column: "SemesterDashbaordId");

            migrationBuilder.CreateIndex(
                name: "IX_MyHoursDashboard_ModulesDashboardId",
                table: "MyHoursDashboard",
                column: "ModulesDashboardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyHoursDashboard");

            migrationBuilder.DropTable(
                name: "SelfStudyHours");

            migrationBuilder.DropTable(
                name: "ModulesDashboard");

            migrationBuilder.DropTable(
                name: "SemesterDashbaord");
        }
    }
}

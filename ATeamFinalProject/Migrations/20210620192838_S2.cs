using Microsoft.EntityFrameworkCore.Migrations;

namespace ATeamFinalProject.Migrations
{
    public partial class S2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProgressReport",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgressReport",
                table: "Enrollments");
        }
    }
}

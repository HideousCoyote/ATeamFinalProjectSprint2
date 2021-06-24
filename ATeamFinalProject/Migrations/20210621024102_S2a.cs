using Microsoft.EntityFrameworkCore.Migrations;

namespace ATeamFinalProject.Migrations
{
    public partial class S2a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tuition = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_LessonId",
                table: "Sessions",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Lessons_LessonId",
                table: "Sessions",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Lessons_LessonId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_LessonId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Sessions");
        }
    }
}

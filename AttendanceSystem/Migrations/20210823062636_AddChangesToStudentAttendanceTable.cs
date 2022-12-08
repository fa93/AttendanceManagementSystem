using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceSystem.Migrations
{
    public partial class AddChangesToStudentAttendanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseTitle",
                table: "StudentAttendances",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseTitle",
                table: "StudentAttendances");
        }
    }
}

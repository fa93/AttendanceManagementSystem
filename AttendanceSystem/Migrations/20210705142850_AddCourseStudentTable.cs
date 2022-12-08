using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceSystem.Migrations
{
    public partial class AddCourseStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssingedCoursesToStudents",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StudnetId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssingedCoursesToStudents", x => new { x.CourseId, x.StudnetId });
                    table.ForeignKey(
                        name: "FK_AssingedCoursesToStudents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssingedCoursesToStudents_Students_StudnetId",
                        column: x => x.StudnetId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssingedCoursesToStudents_StudnetId",
                table: "AssingedCoursesToStudents",
                column: "StudnetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssingedCoursesToStudents");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceSystem.Migrations
{
    public partial class AddChangesInStudentCourseTable_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssingedCoursesToStudents_Students_StudnetId",
                table: "AssingedCoursesToStudents");

            migrationBuilder.RenameColumn(
                name: "StudnetId",
                table: "AssingedCoursesToStudents",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_AssingedCoursesToStudents_StudnetId",
                table: "AssingedCoursesToStudents",
                newName: "IX_AssingedCoursesToStudents_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssingedCoursesToStudents_Students_StudentId",
                table: "AssingedCoursesToStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssingedCoursesToStudents_Students_StudentId",
                table: "AssingedCoursesToStudents");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "AssingedCoursesToStudents",
                newName: "StudnetId");

            migrationBuilder.RenameIndex(
                name: "IX_AssingedCoursesToStudents_StudentId",
                table: "AssingedCoursesToStudents",
                newName: "IX_AssingedCoursesToStudents_StudnetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssingedCoursesToStudents_Students_StudnetId",
                table: "AssingedCoursesToStudents",
                column: "StudnetId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

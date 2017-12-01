using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningSystem.Data.Migrations
{
    public partial class UsersCoursesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourse_Courses_CourseId",
                table: "UserCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourse_AspNetUsers_UserId",
                table: "UserCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourse",
                table: "UserCourse");

            migrationBuilder.RenameTable(
                name: "UserCourse",
                newName: "UsersCourses");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourse_CourseId",
                table: "UsersCourses",
                newName: "IX_UsersCourses_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersCourses",
                table: "UsersCourses",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCourses_Courses_CourseId",
                table: "UsersCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCourses_AspNetUsers_UserId",
                table: "UsersCourses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCourses_Courses_CourseId",
                table: "UsersCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersCourses_AspNetUsers_UserId",
                table: "UsersCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersCourses",
                table: "UsersCourses");

            migrationBuilder.RenameTable(
                name: "UsersCourses",
                newName: "UserCourse");

            migrationBuilder.RenameIndex(
                name: "IX_UsersCourses_CourseId",
                table: "UserCourse",
                newName: "IX_UserCourse_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourse",
                table: "UserCourse",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourse_Courses_CourseId",
                table: "UserCourse",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourse_AspNetUsers_UserId",
                table: "UserCourse",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

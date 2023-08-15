using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timetable.Infrastructure.Persistence.Migrations
{
    public partial class DeleteRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Rooms_TeacherpreferredRoomId",
                schema: "TimeTable",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Semesters_SemesterNo",
                schema: "TimeTable",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_User_userId",
                schema: "TimeTable",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Years_YearNo",
                schema: "TimeTable",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Courses_courseId",
                schema: "TimeTable",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Days_DayNo",
                schema: "TimeTable",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Groups_GroupNo",
                schema: "TimeTable",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Rooms_RoomId",
                schema: "TimeTable",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Times_TimeId",
                schema: "TimeTable",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Role_RoleId",
                schema: "Security",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_TakingSurveyAllowedPeriods_Role_RoleID",
                schema: "TimeTable",
                table: "TakingSurveyAllowedPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPreferenceDayTimes_Days_DayNo",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPreferenceDayTimes_Times_timeId",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPreferenceDayTimes_User_userId",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_User_UserId",
                schema: "Security",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_User_UserId",
                schema: "Security",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Role_RoleId",
                schema: "Security",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_User_UserId",
                schema: "Security",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_User_UserId",
                schema: "Security",
                table: "UserTokens");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Rooms_TeacherpreferredRoomId",
                schema: "TimeTable",
                table: "Courses",
                column: "TeacherpreferredRoomId",
                principalSchema: "TimeTable",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Semesters_SemesterNo",
                schema: "TimeTable",
                table: "Courses",
                column: "SemesterNo",
                principalSchema: "TimeTable",
                principalTable: "Semesters",
                principalColumn: "SemesterNo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_User_userId",
                schema: "TimeTable",
                table: "Courses",
                column: "userId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Years_YearNo",
                schema: "TimeTable",
                table: "Courses",
                column: "YearNo",
                principalSchema: "TimeTable",
                principalTable: "Years",
                principalColumn: "YearNo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Courses_courseId",
                schema: "TimeTable",
                table: "Lectures",
                column: "courseId",
                principalSchema: "TimeTable",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Days_DayNo",
                schema: "TimeTable",
                table: "Lectures",
                column: "DayNo",
                principalSchema: "TimeTable",
                principalTable: "Days",
                principalColumn: "DayNo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Groups_GroupNo",
                schema: "TimeTable",
                table: "Lectures",
                column: "GroupNo",
                principalSchema: "TimeTable",
                principalTable: "Groups",
                principalColumn: "GroupNo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Rooms_RoomId",
                schema: "TimeTable",
                table: "Lectures",
                column: "RoomId",
                principalSchema: "TimeTable",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Times_TimeId",
                schema: "TimeTable",
                table: "Lectures",
                column: "TimeId",
                principalSchema: "TimeTable",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Role_RoleId",
                schema: "Security",
                table: "RoleClaims",
                column: "RoleId",
                principalSchema: "Security",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TakingSurveyAllowedPeriods_Role_RoleID",
                schema: "TimeTable",
                table: "TakingSurveyAllowedPeriods",
                column: "RoleID",
                principalSchema: "Security",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPreferenceDayTimes_Days_DayNo",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes",
                column: "DayNo",
                principalSchema: "TimeTable",
                principalTable: "Days",
                principalColumn: "DayNo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPreferenceDayTimes_Times_timeId",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes",
                column: "timeId",
                principalSchema: "TimeTable",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPreferenceDayTimes_User_userId",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes",
                column: "userId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_User_UserId",
                schema: "Security",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_User_UserId",
                schema: "Security",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Role_RoleId",
                schema: "Security",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "Security",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_User_UserId",
                schema: "Security",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_User_UserId",
                schema: "Security",
                table: "UserTokens",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Rooms_TeacherpreferredRoomId",
                schema: "TimeTable",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Semesters_SemesterNo",
                schema: "TimeTable",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_User_userId",
                schema: "TimeTable",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Years_YearNo",
                schema: "TimeTable",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Courses_courseId",
                schema: "TimeTable",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Days_DayNo",
                schema: "TimeTable",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Groups_GroupNo",
                schema: "TimeTable",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Rooms_RoomId",
                schema: "TimeTable",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Times_TimeId",
                schema: "TimeTable",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Role_RoleId",
                schema: "Security",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_TakingSurveyAllowedPeriods_Role_RoleID",
                schema: "TimeTable",
                table: "TakingSurveyAllowedPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPreferenceDayTimes_Days_DayNo",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPreferenceDayTimes_Times_timeId",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPreferenceDayTimes_User_userId",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_User_UserId",
                schema: "Security",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_User_UserId",
                schema: "Security",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Role_RoleId",
                schema: "Security",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_User_UserId",
                schema: "Security",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_User_UserId",
                schema: "Security",
                table: "UserTokens");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Rooms_TeacherpreferredRoomId",
                schema: "TimeTable",
                table: "Courses",
                column: "TeacherpreferredRoomId",
                principalSchema: "TimeTable",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Semesters_SemesterNo",
                schema: "TimeTable",
                table: "Courses",
                column: "SemesterNo",
                principalSchema: "TimeTable",
                principalTable: "Semesters",
                principalColumn: "SemesterNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_User_userId",
                schema: "TimeTable",
                table: "Courses",
                column: "userId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Years_YearNo",
                schema: "TimeTable",
                table: "Courses",
                column: "YearNo",
                principalSchema: "TimeTable",
                principalTable: "Years",
                principalColumn: "YearNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Courses_courseId",
                schema: "TimeTable",
                table: "Lectures",
                column: "courseId",
                principalSchema: "TimeTable",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Days_DayNo",
                schema: "TimeTable",
                table: "Lectures",
                column: "DayNo",
                principalSchema: "TimeTable",
                principalTable: "Days",
                principalColumn: "DayNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Groups_GroupNo",
                schema: "TimeTable",
                table: "Lectures",
                column: "GroupNo",
                principalSchema: "TimeTable",
                principalTable: "Groups",
                principalColumn: "GroupNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Rooms_RoomId",
                schema: "TimeTable",
                table: "Lectures",
                column: "RoomId",
                principalSchema: "TimeTable",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Times_TimeId",
                schema: "TimeTable",
                table: "Lectures",
                column: "TimeId",
                principalSchema: "TimeTable",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Role_RoleId",
                schema: "Security",
                table: "RoleClaims",
                column: "RoleId",
                principalSchema: "Security",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakingSurveyAllowedPeriods_Role_RoleID",
                schema: "TimeTable",
                table: "TakingSurveyAllowedPeriods",
                column: "RoleID",
                principalSchema: "Security",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPreferenceDayTimes_Days_DayNo",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes",
                column: "DayNo",
                principalSchema: "TimeTable",
                principalTable: "Days",
                principalColumn: "DayNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPreferenceDayTimes_Times_timeId",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes",
                column: "timeId",
                principalSchema: "TimeTable",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPreferenceDayTimes_User_userId",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes",
                column: "userId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_User_UserId",
                schema: "Security",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_User_UserId",
                schema: "Security",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Role_RoleId",
                schema: "Security",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "Security",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_User_UserId",
                schema: "Security",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_User_UserId",
                schema: "Security",
                table: "UserTokens",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

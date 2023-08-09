using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timetable.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TimeTable");

            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Days",
                schema: "TimeTable",
                columns: table => new
                {
                    DayNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayNo);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "TimeTable",
                columns: table => new
                {
                    GroupNo = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupNo);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "TimeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                schema: "TimeTable",
                columns: table => new
                {
                    SemesterNo = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.SemesterNo);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                schema: "TimeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<TimeSpan>(type: "time", nullable: false),
                    End = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Years",
                schema: "TimeTable",
                columns: table => new
                {
                    YearNo = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.YearNo);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TakingSurveyAllowedPeriods",
                schema: "TimeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakingSurveyAllowedPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TakingSurveyAllowedPeriods_Role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherPreferenceDayTimes",
                schema: "TimeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayNo = table.Column<int>(type: "int", nullable: false),
                    timeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherPreferenceDayTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherPreferenceDayTimes_Days_DayNo",
                        column: x => x.DayNo,
                        principalSchema: "TimeTable",
                        principalTable: "Days",
                        principalColumn: "DayNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherPreferenceDayTimes_Times_timeId",
                        column: x => x.timeId,
                        principalSchema: "TimeTable",
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherPreferenceDayTimes_User_userId",
                        column: x => x.userId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.ProviderKey, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_UserLogins_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "TimeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LuctureNumPerWeek = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsElective = table.Column<bool>(type: "bit", nullable: false),
                    YearNo = table.Column<int>(type: "int", nullable: false),
                    SemesterNo = table.Column<int>(type: "int", nullable: false),
                    TeacherpreferredRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Rooms_TeacherpreferredRoomId",
                        column: x => x.TeacherpreferredRoomId,
                        principalSchema: "TimeTable",
                        principalTable: "Rooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_Semesters_SemesterNo",
                        column: x => x.SemesterNo,
                        principalSchema: "TimeTable",
                        principalTable: "Semesters",
                        principalColumn: "SemesterNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_User_userId",
                        column: x => x.userId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Years_YearNo",
                        column: x => x.YearNo,
                        principalSchema: "TimeTable",
                        principalTable: "Years",
                        principalColumn: "YearNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                schema: "TimeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayNo = table.Column<int>(type: "int", nullable: false),
                    GroupNo = table.Column<int>(type: "int", nullable: true),
                    courseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Courses_courseId",
                        column: x => x.courseId,
                        principalSchema: "TimeTable",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lectures_Days_DayNo",
                        column: x => x.DayNo,
                        principalSchema: "TimeTable",
                        principalTable: "Days",
                        principalColumn: "DayNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lectures_Groups_GroupNo",
                        column: x => x.GroupNo,
                        principalSchema: "TimeTable",
                        principalTable: "Groups",
                        principalColumn: "GroupNo");
                    table.ForeignKey(
                        name: "FK_Lectures_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "TimeTable",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lectures_Times_TimeId",
                        column: x => x.TimeId,
                        principalSchema: "TimeTable",
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SemesterNo",
                schema: "TimeTable",
                table: "Courses",
                column: "SemesterNo");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherpreferredRoomId",
                schema: "TimeTable",
                table: "Courses",
                column: "TeacherpreferredRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_userId",
                schema: "TimeTable",
                table: "Courses",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_YearNo",
                schema: "TimeTable",
                table: "Courses",
                column: "YearNo");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_courseId",
                schema: "TimeTable",
                table: "Lectures",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_DayNo",
                schema: "TimeTable",
                table: "Lectures",
                column: "DayNo");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_GroupNo",
                schema: "TimeTable",
                table: "Lectures",
                column: "GroupNo");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_RoomId",
                schema: "TimeTable",
                table: "Lectures",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_TimeId",
                schema: "TimeTable",
                table: "Lectures",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Security",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Security",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TakingSurveyAllowedPeriods_RoleID",
                schema: "TimeTable",
                table: "TakingSurveyAllowedPeriods",
                column: "RoleID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPreferenceDayTimes_DayNo",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes",
                column: "DayNo");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPreferenceDayTimes_timeId",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes",
                column: "timeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPreferenceDayTimes_userId",
                schema: "TimeTable",
                table: "TeacherPreferenceDayTimes",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Security",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Security",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Security",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lectures",
                schema: "TimeTable");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "TakingSurveyAllowedPeriods",
                schema: "TimeTable");

            migrationBuilder.DropTable(
                name: "TeacherPreferenceDayTimes",
                schema: "TimeTable");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "TimeTable");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "TimeTable");

            migrationBuilder.DropTable(
                name: "Days",
                schema: "TimeTable");

            migrationBuilder.DropTable(
                name: "Times",
                schema: "TimeTable");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "TimeTable");

            migrationBuilder.DropTable(
                name: "Semesters",
                schema: "TimeTable");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Years",
                schema: "TimeTable");
        }
    }
}

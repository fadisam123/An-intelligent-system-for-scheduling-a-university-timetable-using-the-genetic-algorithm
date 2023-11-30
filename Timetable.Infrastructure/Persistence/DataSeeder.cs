using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Linq;
using System.Security.Principal;
using Timetable.Domain.Entities;
using Timetable.Domain.Enums;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        private static readonly Random _random = new Random();

        public static async Task SeedDataAsync(AppDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            await SeedRolesAsync(roleManager);
            await SeedAdminUserAsync(userManager);

            await SeedTeachersAsync(userManager);
            await SeedYearsAsync(dbContext);
            await SeedSemestersAsync(dbContext);
            await SeedDaysAsync(dbContext);
            await SeedRoomsAsync(dbContext);
            await SeedTimeAsync(dbContext);
            await SeedTheoryCoursesAsync(dbContext);
            await SeedLapCoursesAsync(dbContext);

            await SeedTeacherPreferenceDayTimeAsync(dbContext);
            await SeedTeacherPreferenceRoomAsync(dbContext);
            await SeedSurveyTimes(dbContext);
        }

        private static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            string[] roles = Enum.GetNames(typeof(RoleEnum));

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role { Name = role });
                }
            }
        }
        private static async Task SeedAdminUserAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any(u => u.Type == UserTypeEnum.Admin))
            {
                // seed admin user
                var adminUser = new User { Name = "المدير", UserName = "admin", Email = "admin@admin.com", Type = UserTypeEnum.Admin };

                var userPassword = "aaaaaa";


                var result = await userManager.CreateAsync(adminUser, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RoleEnum.Admin.ToString());
                }
            }
        }

        private static async Task SeedTeachersAsync(UserManager<User> userManager)
        {
            IdentityResult result;
            if (!userManager.Users.Any(u => u.Type != UserTypeEnum.Admin))
            {
                // seed all teachers
                User[] DepartmentHead = {
                new User{ Name = "محمد", UserName = "u1", Email = "u1@users.com", Type = UserTypeEnum.DepartmentHead },
                new User{ Name = "أحمد", UserName = "u2", Email = "u1@users.com", Type = UserTypeEnum.DepartmentHead },
                new User{ Name = "خالد", UserName = "u3", Email = "u1@users.com", Type = UserTypeEnum.DepartmentHead },
                };
                var DepartmentHeadPassword = "111111";

                for (int i = 0; i < DepartmentHead.Length; i++)
                {
                    result = await userManager.CreateAsync(DepartmentHead[i], DepartmentHeadPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(DepartmentHead[i], RoleEnum.DepartmentHead.ToString());
                    }
                }

                User[] Professor = {
                new User{ Name = "صالح", UserName = "u4", Email = "u1@users.com", Type = UserTypeEnum.Professor },
                new User{ Name = "أحمد", UserName = "u5", Email = "u1@users.com", Type = UserTypeEnum.Professor },
                new User{ Name = "خالد", UserName = "u6", Email = "u1@users.com", Type = UserTypeEnum.Professor },
                new User{ Name = "ريم", UserName = "u7", Email = "u1@users.com", Type = UserTypeEnum.Professor },
                new User{ Name = "ياسين", UserName = "u8", Email = "u1@users.com", Type = UserTypeEnum.Professor },
                new User{ Name = "محمود", UserName = "u9", Email = "u1@users.com", Type = UserTypeEnum.Professor },
                new User{ Name = "بشرى", UserName = "u10", Email = "u1@users.com", Type = UserTypeEnum.Professor },
                };
                var ProfessorPassword = "111111";

                for (int i = 0; i < Professor.Length; i++)
                {
                    result = await userManager.CreateAsync(Professor[i], ProfessorPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(Professor[i], RoleEnum.Professor.ToString());
                    }
                }

                User[] Teacher = {
                new User{ Name = "أيمن", UserName = "u11", Email = "u1@users.com", Type = UserTypeEnum.LapTeacher },
                new User{ Name = "أحمد", UserName = "u12", Email = "u1@users.com", Type = UserTypeEnum.LapTeacher },
                new User{ Name = "خالد", UserName = "u13", Email = "u1@users.com", Type = UserTypeEnum.LapTeacher },
                new User{ Name = "محمد", UserName = "u14", Email = "u1@users.com", Type = UserTypeEnum.LapTeacher },
                new User{ Name = "عبد المعين", UserName = "u15", Email = "u1@users.com", Type = UserTypeEnum.LapTeacher },
                new User{ Name = "محمود", UserName = "u16", Email = "u1@users.com", Type = UserTypeEnum.LapTeacher },
                new User{ Name = "بشرى", UserName = "u17", Email = "u1@users.com", Type = UserTypeEnum.LapTeacher },
                new User{ Name = "سوسن", UserName = "u18", Email = "u1@users.com", Type = UserTypeEnum.LapTeacher },
                new User{ Name = "جودت", UserName = "u19", Email = "u1@users.com", Type = UserTypeEnum.LapTeacher },
                };
                var TeacherPassword = "111111";

                for (int i = 0; i < Teacher.Length; i++)
                {
                    result = await userManager.CreateAsync(Teacher[i], TeacherPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(Teacher[i], RoleEnum.LapTeacher.ToString());
                    }
                }
            }
        }

        private static async Task SeedYearsAsync(AppDbContext dbContext)
        {
            if (!dbContext.Years.Any())
            {
                var years = new List<Year>
                {
                    new Year { YearNo = 1},
                    new Year { YearNo = 2},
                    new Year { YearNo = 3},
                    new Year { YearNo = 4},
                    new Year { YearNo = 5},
                };
                await dbContext.Years.AddRangeAsync(years);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedSemestersAsync(AppDbContext dbContext)
        {
            if (!dbContext.Semesters.Any())
            {
                var semesters = new List<Semester>
                {
                    new Semester { SemesterNo = 1},
                    new Semester { SemesterNo = 2},
                };
                await dbContext.Semesters.AddRangeAsync(semesters);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedDaysAsync(AppDbContext dbContext)
        {
            if (!dbContext.Days.Any())
            {
                var days = new List<Day>
                {
                    new Day { DayNo = 1, Name = "الأحد"},
                    new Day { DayNo = 2, Name = "الأثنين"},
                    new Day { DayNo = 3, Name = "الثلاثاء"},
                    new Day { DayNo = 4, Name = "الأربعاء"},
                    new Day { DayNo = 5, Name = "الخميس"},
                };
                await dbContext.Days.AddRangeAsync(days);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedRoomsAsync(AppDbContext dbContext)
        {
            if (!dbContext.Rooms.Any())
            {
                var rooms = new List<Room>
                {
                    new Room { Name = "مدرج", type = RoomTypeEnum.TheoryRoom },
                    new Room { Name = "قاعة 1", type = RoomTypeEnum.TheoryRoom },
                    new Room { Name = "قاعة 2", type = RoomTypeEnum.TheoryRoom },
                    new Room { Name = "قاعة 2", type = RoomTypeEnum.TheoryRoom },
                    new Room { Name = "قاعة 3", type = RoomTypeEnum.TheoryRoom },
                    new Room { Name = "قاعة 4", type = RoomTypeEnum.TheoryRoom },
                    new Room { Name = "قاعة 5", type = RoomTypeEnum.TheoryRoom },
                    new Room { Name = "قاعة 6", type = RoomTypeEnum.TheoryRoom },

                    new Room { Name = "مخبر الخوارزميات", type = RoomTypeEnum.LapRoom },
                    new Room { Name = "مخبر قواعد المعطيات", type = RoomTypeEnum.LapRoom },
                    new Room { Name = "مخبر البرمجة", type = RoomTypeEnum.LapRoom },
                    new Room { Name = "مخبر الشبكات", type = RoomTypeEnum.LapRoom },
                    new Room { Name = "مخبر الفيزياء", type = RoomTypeEnum.LapRoom },
                    new Room { Name = "مخبر الذكاء", type = RoomTypeEnum.LapRoom },
                    new Room { Name = "مخبر الدارات الالكترونية", type = RoomTypeEnum.LapRoom },
                    new Room { Name = "مخبر PLC", type = RoomTypeEnum.LapRoom },
                };
                await dbContext.Rooms.AddRangeAsync(rooms);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedTimeAsync(AppDbContext dbContext)
        {
            if (!dbContext.Times.Any())
            {
                var times = new List<Time>
                {
                    new Time { Start = new TimeOnly(9,0), End = new TimeOnly(10,30) },
                    new Time { Start = new TimeOnly(10,45), End = new TimeOnly(12,15) },
                    new Time { Start = new TimeOnly(12,30), End = new TimeOnly(14,0) },
                    new Time { Start = new TimeOnly(14,15), End = new TimeOnly(15,45) },
                    new Time { Start = new TimeOnly(16,0), End = new TimeOnly(17,30) },
                };
                await dbContext.Times.AddRangeAsync(times);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedTheoryCoursesAsync(AppDbContext dbContext)
        {
            if (!dbContext.Courses.Any(c => c.Type == CourseTypeEnum.TheoryCourse))
            {
                var courses = new List<Course>
                {
                    // year 1 semester 1
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u7"),
                        Name = "ثقافة"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u8"),
                        Name = "رياضيات متقطعة"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u9"),
                        Name = "1 فيزياء"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u4"),
                        Name = "لغة 1"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u5"),
                        Name = "مبادئ عمل حواسيب"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u6"),
                        Name = "تحليل 1"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u7"),
                        Name = "برمجة 1"
                    },

                    // year 1 semester 2
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u8"),
                        Name = "تحليل 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u9"),
                        Name = "دارات كهربائية"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u10"),
                        Name = "لغة عربية"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u10"),
                        Name = "برمجة 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u4"),
                        Name = "جبر خطي"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u5"),
                        Name = "لغة 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u4"),
                        Name = "فيزياء 2"
                    },

                    // year 2 semester 1
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u5"),
                        Name = "لغة 3"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u6"),
                        Name = "برمجة 3"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u7"),
                        Name = "تحليل 3"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u8"),
                        Name = "الكترونيات"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u9"),
                        Name = "برمجة رياضية"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u10"),
                        Name = "تحليل عددي 1"
                    },

                    // year 2 semester 2
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u1"),
                        Name = "تحليل 4"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u2"),
                        Name = "دارات منطقية"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u3"),
                        Name = "مهارات تواصل"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u4"),
                        Name = "خوارزميات 1"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u5"),
                        Name = "لغة 4"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u6"),
                        Name = "تحليل عددي 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u7"),
                        Name = "إحصاء"
                    },

                    // year 3 semester 1
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u8"),
                        Name = "رسوميات حاسوبية"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u9"),
                        Name = "معالج مصغر"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u10"),
                        Name = "خوارزميات 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u1"),
                        Name = "نظرية معلومات"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u2"),
                        Name = "معالجة اشارة"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u3"),
                        Name = "مخططات"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u4"),
                        Name = "معطيات 1"
                    },

                    // year 3 semester 2
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u5"),
                        Name = "شبكات حاسوبية"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u6"),
                        Name = "بنية 1"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u7"),
                        Name = "اتصالات"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u8"),
                        Name = "مبادئ ذكاء"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u9"),
                        Name = "صورية"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u10"),
                        Name = "برمجيات 1"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u1"),
                        Name = "خوارزميات 3"
                    },

                    // year 4 semester 1
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u2"),
                        Name = "أرتال"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u3"),
                        Name = "بنية 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u4"),
                        Name = "معطيات 2",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u5"),
                        Name = "شبكات متقدمة",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u6"),
                        Name = "برمجة منطقية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u5"),
                        Name = "تصميم مترجمات"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u6"),
                        Name = "نظم وسائط"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u7"),
                        Name = "بحوث عمليات"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u8"),
                        Name = "نظم تشغيل 1"
                    },

                    // year 4 semester 2
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u9"),
                        Name = "أمن معلومات"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u10"),
                        Name = "روبوتية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u1"),
                        Name = "برمجيات 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u2"),
                        Name = "تفرعية"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u3"),
                        Name = "عصبونية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u4"),
                        Name = "تسويق"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u5"),
                        Name = "نظم رقمية"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u6"),
                        Name = "نظم تشغيل 2",
                        IsElective = true
                    },

                    // year 5 semester 1
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u7"),
                        Name = "نظم خبيرة",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u7"),
                        Name = "نظم موزعة",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u8"),
                        Name = "نمذجة ومحاكاة"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u9"),
                        Name = "تحكم منطقي PLC"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u10"),
                        Name = "أمن شبكات",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u10"),
                        Name = "رؤية حاسوبية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u9"),
                        Name = "برمجيات 3",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u1"),
                        Name = "جودة وموثوقية"
                    },

                    // year 5 semester 2
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u2"),
                        Name = "زمن حقيقي"
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u3"),
                        Name = "معالجة لغات طبيعية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u4"),
                        Name = "شبكات لاسلكية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u5"),
                        Name = "تنقيب معطيات",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.TheoryCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u6"),
                        Name = "نظم انتاجية"
                    },
                };
                await dbContext.Courses.AddRangeAsync(courses);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedLapCoursesAsync(AppDbContext dbContext)
        {
            if (!dbContext.Courses.Any(c => c.Type == CourseTypeEnum.LapCourse))
            {
                var Rooms = dbContext.Rooms.Where(r => r.type == RoomTypeEnum.LapRoom || r.type == RoomTypeEnum.MixedRoom).ToList();

                var courses = new List<Course>
                {
                    // year 1 semester 1
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "رياضيات متقطعة",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "1 فيزياء",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "مبادئ عمل حواسيب",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "تحليل 1",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "برمجة 1",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },

                    // year 1 semester 2
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "تحليل 2",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "دارات كهربائية",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "برمجة 2",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "جبر خطي",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "فيزياء 2",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },

                    // year 2 semester 1
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "برمجة 3",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "تحليل 3",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "الكترونيات",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "برمجة رياضية",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "تحليل عددي 1",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },

                    // year 2 semester 2
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "تحليل 4",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "دارات منطقية",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "خوارزميات 1",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "تحليل عددي 2",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "إحصاء",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },

                    // year 3 semester 1
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "رسوميات حاسوبية",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "معالج مصغر",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "خوارزميات 2",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "نظرية معلومات",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "معالجة اشارة",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "مخططات",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "معطيات 1",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },

                    // year 3 semester 2
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "شبكات حاسوبية",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "بنية 1",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "اتصالات",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "مبادئ ذكاء",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "صورية",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "برمجيات 1",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "خوارزميات 3",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },

                    // year 4 semester 1
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "أرتال",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "بنية 2",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "معطيات 2",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "شبكات متقدمة",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "برمجة منطقية",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "تصميم مترجمات",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "نظم وسائط",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "بحوث عمليات",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "نظم تشغيل 1",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },

                    // year 4 semester 2
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "أمن معلومات",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "روبوتية",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "برمجيات 2",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "تفرعية",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "عصبونية",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "تسويق",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "نظم رقمية",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "نظم تشغيل 2",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },

                    // year 5 semester 1
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "نظم خبيرة",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "نظم موزعة",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "نمذجة ومحاكاة",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "تحكم منطقي PLC",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "برمجيات 3",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "رؤية حاسوبية",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "أمن شبكات",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "جودة وموثوقية",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },

                    // year 5 semester 2
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "زمن حقيقي",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "معالجة لغات طبيعية",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "شبكات لاسلكية",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "تنقيب معطيات",
                        IsElective = true,
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "نظم انتاجية",
                        TeacherpreferredRoom = Rooms[_random.Next(0, Rooms.Count())]
                    },
                };
                await dbContext.Courses.AddRangeAsync(courses);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedTeacherPreferenceDayTimeAsync(AppDbContext dbContext)
        {
            if (!dbContext.TeacherPreferenceDayTimes.Any() && dbContext.Days.Any() && dbContext.Times.Any())
            {
                var teachers = dbContext.Users.Where(t => t.Courses.Any()).Include(t => t.Courses).ToList();
                foreach (var teacher in teachers)
                {
                    var courses = teacher.Courses.Where(c => c.semester.SemesterNo == 1);
                    foreach (var course in courses)
                    {
                        int repeate = _random.Next(1, 3);
                        for (int i = 0; i < repeate; i++)
                        {
                            var Days = dbContext.Days.ToList();
                            var Times = dbContext.Times.ToList();

                            int randomDayIndex = _random.Next(0, Days.Count());
                            Day randomDay = Days[randomDayIndex];

                            int randomTimeIndex = _random.Next(0, Times.Count());
                            Time randomTime = Times[randomTimeIndex];

                            var tpdt = new TeacherPreferenceDayTime
                            {
                                user = teacher,
                                day = randomDay,
                                time = randomTime
                            };

                            await dbContext.TeacherPreferenceDayTimes.AddAsync(tpdt);
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
            }
        }

        private static async Task SeedTeacherPreferenceRoomAsync(AppDbContext dbContext)
        {
            if (dbContext.Courses.Any() && dbContext.Rooms.Any())
            {
                var courses = dbContext.Courses.Where(c => c.semester.SemesterNo == 1 && c.Type == CourseTypeEnum.TheoryCourse && c.user != null).ToList();
                foreach (var course in courses)
                {
                    if (course.TeacherpreferredRoom is null)
                    {
                        var Rooms = dbContext.Rooms.Where(r => r.type == RoomTypeEnum.TheoryRoom || r.type == RoomTypeEnum.MixedRoom).ToList();
                        int randomRoomIndex = _random.Next(0, Rooms.Count());
                        Room randomRoom = Rooms[randomRoomIndex];

                        course.TeacherpreferredRoom = randomRoom;

                        dbContext.Entry(course).State = EntityState.Modified;
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
        }

        private static async Task SeedSurveyTimes(AppDbContext dbContext)
        {
            if (!dbContext.TakingSurveyAllowedPeriods.Any())
            {
                TakingSurveyAllowedPeriod temp = new TakingSurveyAllowedPeriod
                {
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(100),
                    role = dbContext.Roles.Where(r => r.Name.ToUpper() == RoleEnum.DepartmentHead.ToString().ToUpper()).First()
                };
                dbContext.Add(temp);
                dbContext.SaveChanges();

                temp = new TakingSurveyAllowedPeriod
                {
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(100),
                    role = dbContext.Roles.Where(r => r.Name.ToUpper() == RoleEnum.Professor.ToString().ToUpper()).First()
                };
                dbContext.Add(temp);
                dbContext.SaveChanges();

                temp = new TakingSurveyAllowedPeriod
                {
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(100),
                    role = dbContext.Roles.Where(r => r.Name.ToUpper() == RoleEnum.LapTeacher.ToString().ToUpper()).First()
                };
                dbContext.Add(temp);
                dbContext.SaveChanges();
            }
        }
    }
}

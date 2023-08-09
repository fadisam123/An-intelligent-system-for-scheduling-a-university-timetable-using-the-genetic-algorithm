using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Timetable.Domain.Entities;
using Timetable.Domain.Enums;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Infrastructure.Persistence
{
    public static class DataSeeder
    {

        public static async Task SeedData(AppDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            await SeedRoles(roleManager);
            await SeedUsers(userManager);

            await SeedYears(dbContext);
            await SeedSemesters(dbContext);
            await SeedDays(dbContext);
            await SeedRooms(dbContext);
            await SeedTime(dbContext);
            await SeedTheoryCourses(dbContext);
            await SeedLapCourses(dbContext);
        }

        private static async Task SeedRoles(RoleManager<Role> roleManager)
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

        private static async Task SeedUsers(UserManager<User> userManager)
        {
            // seed admin user
            var adminUser = new User { Name = "المدير", UserName = "admin", Email = "admin@admin.com", Type = UserTypeEnum.Admin };

            var userPassword = "admin123"; // Replace this with a strong password for the user.

            var existingAdmin = await userManager.FindByNameAsync(adminUser.UserName);
            if (existingAdmin == null)
            {
                var result = await userManager.CreateAsync(adminUser, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RoleEnum.Admin.ToString());
                }
            }

            // seed all teachers
            User[] DepartmentHead = {
                new User{ Name = "محمد", UserName = "u1", Email = "u1@users.com", Type = UserTypeEnum.DepartmentHead },
                new User{ Name = "أحمد", UserName = "u2", Email = "u1@users.com", Type = UserTypeEnum.DepartmentHead },
                new User{ Name = "خالد", UserName = "u3", Email = "u1@users.com", Type = UserTypeEnum.DepartmentHead },
            };
            var DepartmentHeadPassword = "user123";

            for (int i = 0; i < DepartmentHead.Length; i++)
            {
                var result = await userManager.CreateAsync(DepartmentHead[i], DepartmentHeadPassword);
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
            var ProfessorPassword = "user123";

            for (int i = 0; i < Professor.Length; i++)
            {
                var result = await userManager.CreateAsync(Professor[i], ProfessorPassword);
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
            var TeacherPassword = "user123";

            for (int i = 0; i < Teacher.Length; i++)
            {
                var result = await userManager.CreateAsync(Teacher[i], TeacherPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(Teacher[i], RoleEnum.LapTeacher.ToString());
                }
            }
        }

        private static async Task SeedYears(AppDbContext dbContext)
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

        private static async Task SeedSemesters(AppDbContext dbContext)
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

        private static async Task SeedDays(AppDbContext dbContext)
        {
            if (!dbContext.Semesters.Any())
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

        private static async Task SeedRooms(AppDbContext dbContext)
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

        private static async Task SeedTime(AppDbContext dbContext)
        {
            if (!dbContext.Times.Any())
            {
                var times = new List<Time>
                {
                    new Time { Start = new TimeOnly(9,0), End = new TimeOnly(10,30) },
                    new Time { Start = new TimeOnly(10,45), End = new TimeOnly(12,15) },
                    new Time { Start = new TimeOnly(12,30), End = new TimeOnly(2,0) },
                    new Time { Start = new TimeOnly(2,15), End = new TimeOnly(3,45) },
                    new Time { Start = new TimeOnly(4,0), End = new TimeOnly(5,30) },
                };
                await dbContext.Times.AddRangeAsync(times);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedTheoryCourses(AppDbContext dbContext)
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

        private static async Task SeedLapCourses(AppDbContext dbContext)
        {
            if (!dbContext.Courses.Any(c => c.Type == CourseTypeEnum.LapCourse))
            {
                var courses = new List<Course>
                {
                    // year 1 semester 1
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "رياضيات متقطعة"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "1 فيزياء"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "مبادئ عمل حواسيب"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "تحليل 1"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "برمجة 1"
                    },

                    // year 1 semester 2
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "تحليل 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "دارات كهربائية"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "برمجة 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "جبر خطي"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 1),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "فيزياء 2"
                    },

                    // year 2 semester 1
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "برمجة 3"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "تحليل 3"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "الكترونيات"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "برمجة رياضية"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "تحليل عددي 1"
                    },

                    // year 2 semester 2
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "تحليل 4"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "دارات منطقية"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "خوارزميات 1"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "تحليل عددي 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 2),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "إحصاء"
                    },

                    // year 3 semester 1
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "رسوميات حاسوبية"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "معالج مصغر"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "خوارزميات 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "نظرية معلومات"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "معالجة اشارة"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "مخططات"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "معطيات 1"
                    },

                    // year 3 semester 2
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "شبكات حاسوبية"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "بنية 1"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "اتصالات"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "مبادئ ذكاء"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "صورية"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "برمجيات 1"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 3),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "خوارزميات 3"
                    },

                    // year 4 semester 1
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "أرتال"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "بنية 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "معطيات 2",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "شبكات متقدمة",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "برمجة منطقية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "تصميم مترجمات"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "نظم وسائط"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "بحوث عمليات"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "نظم تشغيل 1"
                    },

                    // year 4 semester 2
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 2,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "أمن معلومات"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "روبوتية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "برمجيات 2"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "تفرعية"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u11"),
                        Name = "عصبونية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "تسويق"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "نظم رقمية"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 4),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "نظم تشغيل 2",
                        IsElective = true
                    },

                    // year 5 semester 1
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "نظم خبيرة",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "نظم موزعة",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u16"),
                        Name = "نمذجة ومحاكاة"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u17"),
                        Name = "تحكم منطقي PLC"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "برمجيات 3",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u18"),
                        Name = "رؤية حاسوبية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "أمن شبكات",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 1),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u19"),
                        Name = "جودة وموثوقية"
                    },

                    // year 5 semester 2
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u1"),
                        Name = "زمن حقيقي"
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u12"),
                        Name = "معالجة لغات طبيعية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u13"),
                        Name = "شبكات لاسلكية",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u14"),
                        Name = "تنقيب معطيات",
                        IsElective = true
                    },
                    new Course {
                        Type = CourseTypeEnum.LapCourse,
                        year = dbContext.Years.First(y => y.YearNo == 5),
                        semester = dbContext.Semesters.First(s => s.SemesterNo == 2),
                        LuctureNumPerWeek = 1,
                        user = dbContext.Users.First(u => u.UserName == "u15"),
                        Name = "نظم انتاجية"
                    },
                };
                await dbContext.Courses.AddRangeAsync(courses);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}

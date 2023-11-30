using Model;

namespace MemoryDB
{
    public sealed class DB
    {
        private static DB instance;
        private static readonly object lockObject = new object();

        public List<Teacher> teachers = new List<Teacher>();
        public List<ClassRoom> classRooms = new List<ClassRoom>();
        public List<Course> Courses = new List<Course>();

        private DB()
        {
            Random random = new Random();
            for (int i = 1; i < 9; i++)
            {
                Teacher t = new Teacher("Teacher" + i);
                teachers.Add(t);

                ClassRoom cr = new ClassRoom("Room" + i, 300, false);
                classRooms.Add(cr);
            }
            for (int i = 1; i <= 62; i++)
            {
                if (i <= 14)
                {
                    int lucPerWeek = 1;
                    int semester = 1;
                    if (random.NextDouble() < 0.15)
                    {
                        lucPerWeek = 2;
                    }
                    if (i % 2 == 0)
                    {
                        semester = 2;
                    }
                    Course c = new Course(
                        "Course" + i,
                        1, semester,
                        lucPerWeek,
                        teachers.ElementAt(random.Next(0, 8)),
                        classRooms[random.Next(0, 8)]
                        );
                    Courses.Add(c);
                }
                if (i > 14 && i <= 28)
                {
                    int lucPerWeek = 1;
                    int semester = 1;
                    if (random.NextDouble() < 0.15)
                    {
                        lucPerWeek = 2;
                    }
                    if (i % 2 == 0)
                    {
                        semester = 2;
                    }
                    Course c = new Course(
                        "Course" + i,
                        2, semester,
                        lucPerWeek,
                        teachers.ElementAt(random.Next(0, 8)),
                        classRooms[random.Next(0, 8)]
                        );
                    Courses.Add(c);
                }
                if (i > 28 && i <= 42)
                {
                    int lucPerWeek = 1;
                    int semester = 1;
                    if (random.NextDouble() < 0.15)
                    {
                        lucPerWeek = 2;
                    }
                    if (i % 2 == 0)
                    {
                        semester = 2;
                    }
                    Course c = new Course(
                        "Course" + i,
                        3, semester,
                        lucPerWeek,
                        teachers.ElementAt(random.Next(0, 8)),
                        classRooms[random.Next(0, 8)]
                        );
                    Courses.Add(c);
                }
                if (i > 42 && i <= 54)
                {
                    int lucPerWeek = 1;
                    int semester = 1;
                    if (random.NextDouble() < 0.15)
                    {
                        lucPerWeek = 2;
                    }
                    if (i % 2 == 0)
                    {
                        semester = 2;
                    }
                    Course c = new Course(
                        "Course" + i,
                        4, semester,
                        lucPerWeek,
                        teachers.ElementAt(random.Next(0, 8)),
                        classRooms[random.Next(0, 8)]
                        );
                    Courses.Add(c);
                }
                if (i > 54)
                {
                    int lucPerWeek = 1;
                    int semester = 1;
                    if (random.NextDouble() < 0.15)
                    {
                        lucPerWeek = 2;
                    }
                    if (i % 2 == 0)
                    {
                        semester = 2;
                    }
                    Course c = new Course(
                        "Course" + i,
                        5, semester,
                        lucPerWeek,
                        teachers.ElementAt(random.Next(0, 8)),
                        classRooms[random.Next(0, 8)]
                        );
                    Courses.Add(c);
                }
            }

            // this will reach 100% satisfaction (no teacher have same preference with other -- all unique)
            var teachersCourses = Courses.Where(c => c.Semester == 1).GroupBy(c => c.Teacher.Id);
            foreach (var teacherCourses in teachersCourses)
            {
                List<PreferredDayTime> lpdt = new List<PreferredDayTime>();
                for (int i = 0; i < teacherCourses.Count(); i++)
                {
                    PreferredDayTime pdt;
                again: pdt = new PreferredDayTime((byte)random.Next(1, 6), new TimeOnly(random.Next(9, 17), 0));
                    // we assure that no teacher have same preference with other -- all preferences are unique
                    for (int j = 0; j < teachers.Count; j++)
                    {
                        if (teachers[j].ListOfPreferredDayTime != null && teachers[j].ListOfPreferredDayTime.Where(l => l.preferredDay == pdt.preferredDay && l.preferredTime == pdt.preferredTime).Count() != 0)
                            goto again;
                    }
                    lpdt.Add(pdt);
                    // if the course have two lecture per week then add one more preference
                    if (teacherCourses.ToList()[i].Lecture_num_per_week == 2)
                    {
                    again2: pdt = new PreferredDayTime((byte)random.Next(1, 6), new TimeOnly(random.Next(9, 17), 0));
                        // we assure that no teacher have same preference with other -- all preferences are unique
                        for (int j = 0; j < teachers.Count; j++)
                        {
                            if (teachers[j].ListOfPreferredDayTime != null && teachers[j].ListOfPreferredDayTime.Where(l => l.preferredDay == pdt.preferredDay && l.preferredTime == pdt.preferredTime).Count() != 0)
                                goto again2;
                        }
                        lpdt.Add(pdt);
                    }
                }
                teachers.Where(t => t.Id == teacherCourses.Key).First().ListOfPreferredDayTime = lpdt;
            }

            //// this will not reach 100% satisfaction (more than one teacher has the same preference)
            //var teachersCourses = Courses.Where(c => c.Semester == 1).GroupBy(c => c.Teacher.Id);
            //foreach (var teacherCourses in teachersCourses)
            //{
            //    List<PreferredDayTime> lpdt = new List<PreferredDayTime>();
            //    for (int i = 0; i < teacherCourses.Count(); i++)
            //    {
            //        PreferredDayTime pdt;
            //        pdt = new PreferredDayTime((byte)random.Next(1, 6), new TimeOnly(random.Next(9, 17), 0));
            //        lpdt.Add(pdt);
            //        // if the course have two lecture per week then add one more preference
            //        if (teacherCourses.ToList()[i].Lecture_num_per_week == 2)
            //        {
            //            pdt = new PreferredDayTime((byte)random.Next(1, 6), new TimeOnly(random.Next(9, 17), 0));
            //            lpdt.Add(pdt);
            //        }
            //    }
            //    teachers.Where(t => t.Id == teacherCourses.Key).First().ListOfPreferredDayTime = lpdt;
            //}
            //teachers.First().ListOfPreferredDayTime[0].preferredDay = teachers.Last().ListOfPreferredDayTime[0].preferredDay;
            //teachers.First().ListOfPreferredDayTime[0].preferredTime = teachers.Last().ListOfPreferredDayTime[0].preferredTime;
            //teachers[1].ListOfPreferredDayTime[0].preferredDay = teachers[2].ListOfPreferredDayTime[0].preferredDay;
            //teachers[1].ListOfPreferredDayTime[0].preferredTime = teachers[2].ListOfPreferredDayTime[0].preferredTime;
            //teachers[1].ListOfPreferredDayTime[0].preferredDay = teachers[3].ListOfPreferredDayTime[0].preferredDay;
            //teachers[1].ListOfPreferredDayTime[0].preferredTime = teachers[3].ListOfPreferredDayTime[0].preferredTime;
        }

        public static DB Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new DB();
                        }
                    }
                }
                return instance;
            }
        }

    }
}
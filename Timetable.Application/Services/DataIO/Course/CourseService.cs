using Timetable.Application.Persistence.UnitOfWork;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Application.Services.DataIO.Course
{
    using System.Linq;
    using Timetable.Domain.Entities;
    public class CourseService : ICourseService
    {
        private IUnitOfWork Uow { get; }
        public CourseService(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public async Task createCourseAsync(Course course)
        {
            Uow.CourseRepository.Add(course);
            Uow.SaveChanges();
        }

        public IEnumerable<Course> getAllTheoryCourses(Semester semester, Year year)
        {
            return Uow.CourseRepository.getAllTheoryCourses(semester, year);
        }
        public IEnumerable<Course> getAllTheoryCourses()
        {
            return Uow.CourseRepository.getAllTheoryCourses();
        }

        public Course getCourseById(Guid courseId)
        {
            return Uow.CourseRepository.GetById(courseId);
        }

        public IEnumerable<Year> getAllYears()
        {
            return Uow.YearRepository.GetAll().OrderBy(y => y.YearNo);
        }

        public IEnumerable<Semester> getAllSemesters()
        {
            return Uow.SemesterRepository.GetAll().OrderBy(s => s.SemesterNo);
        }

        public bool HasPracticalSection(Course theoryCourse)
        {
            if (theoryCourse.Type != CourseTypeEnum.TheoryCourse)
                throw new NotImplementedException("Course must be of theory type");

            return Uow.CourseRepository.Find(c => c.Name == theoryCourse.Name &&
                c.Type == CourseTypeEnum.LapCourse).Any();
        }

        public Year? getYear(int yearNo)
        {
            return Uow.YearRepository.Find(y => y.YearNo == yearNo).FirstOrDefault();
        }

        public Semester? getSemester(int semesterNo)
        {
            return Uow.SemesterRepository.Find(s => s.SemesterNo == semesterNo).FirstOrDefault();
        }

        public Course? GetCorrespondingLabCourse(Course theoryCourse)
        {
            return Uow.CourseRepository.Find(c => c.Name.ToUpper() == theoryCourse.Name.ToUpper() && c.Type == CourseTypeEnum.LapCourse).FirstOrDefault();
        }

        public IEnumerable<Course> getAllLabCourses()
        {
            return Uow.CourseRepository.Find(c => c.Type == CourseTypeEnum.LapCourse);
        }

        public async Task AssignLabCourseToTeacherAsync(Course LabCourse, User Labteacher, Room room)
        {
            LabCourse.user = Labteacher;
            LabCourse.TeacherpreferredRoom = room;
            Uow.CourseRepository.Update(LabCourse);
            Uow.SaveChanges();
        }

        public async Task AssignTheoryCourseToTeacherAsync(Course TheoryCourse, User TheoryTeacher)
        {
            TheoryCourse.user = TheoryTeacher;
            Uow.CourseRepository.Update(TheoryCourse);
            Uow.SaveChanges();
        }

        public bool CheckCoursesExistWithAssignedUsers()
        {
            return Uow.CourseRepository.CheckCoursesExistWithAssignedUsers();
        }

        public IEnumerable<Course> getAllTeacherSemesterCourses(User teacher, Semester semester)
        {
            return Uow.CourseRepository.Find(c => c.semester.SemesterNo == semester.SemesterNo && c.user.Id == teacher.Id).OrderBy(c => c.semester).ThenBy(c => c.year.YearNo);
        }

        public void AssignTeacherPreferredRoom(Course TeacherCourse, Room PreferredRoom)
        {
            TeacherCourse.TeacherpreferredRoom = PreferredRoom;
            Uow.CourseRepository.Update(TeacherCourse);
            Uow.SaveChanges();
        }

        public IEnumerable<Course> getAllAssignedLabCourses()
        {
            return Uow.CourseRepository.Find(c => c.Type == CourseTypeEnum.LapCourse && c.user != null && c.TeacherpreferredRoom != null);
        }
        public IEnumerable<Course> getAllNotAssignedLabCourses()
        {
            return Uow.CourseRepository.Find(c => c.Type == CourseTypeEnum.LapCourse && c.user == null && c.TeacherpreferredRoom == null);
        }

        public IEnumerable<Course> getAllAssignedTheoryCourses()
        {
            return Uow.CourseRepository.Find(c => c.Type == CourseTypeEnum.TheoryCourse && c.user != null);
        }
        public IEnumerable<Course> getAllNotAssignedTheoryCourses()
        {
            return Uow.CourseRepository.Find(c => c.Type == CourseTypeEnum.TheoryCourse && c.user == null);
        }

        public async Task RemoveAssignedLapCourseTeacher(Guid labCourseId)
        {
            var course = Uow.CourseRepository.GetById(labCourseId);
            _ = course.user;
            _ = course.TeacherpreferredRoom;
            course.TeacherpreferredRoom = null;
            course.user = null;
            Uow.CourseRepository.Update(course);
            Uow.SaveChanges();
        }
        public async Task RemoveAssignedTheoryCourseTeacher(Guid theoryCourseId)
        {
            var course = Uow.CourseRepository.GetById(theoryCourseId);
            _ = course.user;
            course.user = null;
            Uow.CourseRepository.Update(course);
            Uow.SaveChanges();
        }

        public void DeleteCourseById(Guid courseId)
        {
            Uow.CourseRepository.Remove(getCourseById(courseId));
            Uow.SaveChanges();
        }
    }
}

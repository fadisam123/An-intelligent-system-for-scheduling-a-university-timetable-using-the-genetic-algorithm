using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Application.Services.DataIO.Course
{
    using Timetable.Application.Persistence.UnitOfWork;
    using Timetable.Domain.Entities;
    public interface ICourseService
    {
        public Task createCourseAsync(Course course);
        public Course getCourseById(Guid teacherId);
        public Course GetCorrespondingLabCourse(Course theoryCourse);
        public IEnumerable<Course> getAllTheoryCourses();
        public IEnumerable<Course> getAllLabCourses();
        public IEnumerable<Year> getAllYears();
        public Year getYear(int YearNo);
        public IEnumerable<Semester> getAllSemesters();
        public Semester getSemester(int SemesterNo);
        public bool HasPracticalSection(Course theoryCourse);
        public Task AssignLabCourseToTeacherAsync(Course LabCourse, User Labteacher, Room room);
    }
}

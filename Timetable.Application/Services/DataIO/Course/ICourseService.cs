using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Application.Services.DataIO.Course
{
    using Timetable.Application.Persistence.UnitOfWork;
    using Timetable.Domain.Entities;
    public interface ICourseService
    {
        public Task createCourseAsync(Course course);
        public Course getCourseById(Guid teacherId);
        public IEnumerable<Course> getAllTheoryCourses();
        public IEnumerable<Year> getAllYears();
        public IEnumerable<Semester> getAllSemesters();
        public bool HasPracticalSection(Course theoryCourse);
    }
}

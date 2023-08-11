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
    }
}

using Microsoft.EntityFrameworkCore;
using Timetable.Application.Persistence.Repository;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }

        public bool CheckCoursesExistWithAssignedUsers()
        {
            return _context.Courses.Any(c => c.user != null);
        }

        public IEnumerable<Course> getAllTheoryCourses()
        {
            return _context.Courses.Where(c => c.Type == CourseTypeEnum.TheoryCourse)
                .Include(c => c.year).Include(c => c.semester).OrderBy(c => c.semester)
                .ThenBy(c => c.year).ThenByDescending(c => c.CreatedAt).ToList();
        }

        public IEnumerable<Course> getAllTheoryCourses(Semester semester, Year year)
        {
            return _context.Courses.Where(c => c.Type == CourseTypeEnum.TheoryCourse && c.semester.SemesterNo == semester.SemesterNo && c.year.YearNo == year.YearNo)
                .Include(c => c.year).Include(c => c.semester).OrderBy(c => c.semester)
                .ThenBy(c => c.year).ThenByDescending(c => c.CreatedAt).ToList();
        }
    }
}

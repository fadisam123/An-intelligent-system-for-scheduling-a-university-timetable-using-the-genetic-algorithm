namespace Timetable.Application.Persistence.Repository
{
    public interface ICourseRepository: IGenericRepository<Course>
    {
        IEnumerable<Course> getAllTheoryCourses();
        bool CheckCoursesExistWithAssignedUsers();
    }
}

﻿namespace Timetable.Application.Persistence.Repository
{
    public interface ICourseRepository: IGenericRepository<Course>
    {
        IEnumerable<Course> getAllTheoryCourses();
        IEnumerable<Course> getAllTheoryCourses(Semester semester, Year year);
        bool CheckCoursesExistWithAssignedUsers();
    }
}

namespace Timetable.Application.Services.DataIO.Course
{
    using Timetable.Domain.Entities;
    public interface ICourseService
    {
        public Task createCourseAsync(Course course);
        public Course getCourseById(Guid teacherId);
        public Course? GetCorrespondingLabCourse(Course theoryCourse);
        public IEnumerable<Course> getAllTheoryCourses(Semester semester, Year year);
        public IEnumerable<Course> getAllTheoryCourses();
        public IEnumerable<Course> getAllLabCourses();
        public IEnumerable<Course> getAllAssignedLabCourses();
        public IEnumerable<Course> getAllNotAssignedLabCourses();
        public IEnumerable<Course> getAllAssignedTheoryCourses();
        public IEnumerable<Course> getAllNotAssignedTheoryCourses();
        public IEnumerable<Course> getAllTeacherSemesterCourses(User teacher, Semester semester);
        public IEnumerable<Year> getAllYears();
        public Year? getYear(int YearNo);
        public IEnumerable<Semester> getAllSemesters();
        public Semester? getSemester(int SemesterNo);
        public bool HasPracticalSection(Course theoryCourse);
        public Task AssignLabCourseToTeacherAsync(Course LabCourse, User Labteacher, Room room);
        public Task RemoveAssignedLapCourseTeacher(Guid labCourseId);
        public Task RemoveAssignedTheoryCourseTeacher(Guid theoryCourseId);
        public Task AssignTheoryCourseToTeacherAsync(Course TheoryCourse, User TheoryTeacher);
        public void AssignTeacherPreferredRoom(Course TeacherCourse, Room PreferredRoom);
        public bool CheckCoursesExistWithAssignedUsers();
        public void DeleteCourseById(Guid courseId);
        public void Update(Course course);
    }
}

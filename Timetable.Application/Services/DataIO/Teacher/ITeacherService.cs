using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Application.Services.DataIO.Teacher
{
    public interface ITeacherService
    {
        public Task createTeacherAsync(User user, string password);
        public User getTeacherById(Guid teacherId);
        public IEnumerable<User> getAllTeachers();
        public IEnumerable<User> getAllLabTeachers();
        public IEnumerable<User> getAllTheoryTeachers();
        public void DeleteTeacherById(Guid teacherId);
        public void UpdateTeacher(User teacher);
        public Task UpdateTeacher(User teacher, string newPassword);
    }
}

using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Application.Services.DataIO.Teacher
{
    public interface ITeacherService
    {
        public Task<User> createTeacherAsync(string name, UserTypeEnum teacherType, string userName, string password);
        public User getTeacherById(Guid teacherId);
        public IEnumerable<User> getAllTeachers();
    }
}

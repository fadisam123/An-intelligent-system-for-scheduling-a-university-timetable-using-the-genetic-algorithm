using Microsoft.AspNetCore.Identity;
using Timetable.Application.Persistence.UnitOfWork;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Application.Services.DataIO.Teacher
{
    public class TeacherService : ITeacherService
    {
        private IUnitOfWork Uow { get; }
        private UserManager<User> UserManager { get; }
        public TeacherService(IUnitOfWork uow, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            Uow = uow;
            UserManager = userManager;
        }

        public async Task createTeacherAsync(User user, string password)
        {
            if (await UserManager.FindByNameAsync(user.UserName) is null)
            {
                var userResult = await UserManager.CreateAsync(user, password);
                if (userResult.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, user.Type.ToString());
                    return;
                }
                else
                {
                    throw new NotImplementedException(message: "error in creating the teacher");
                }
            }
            throw new NotImplementedException(message: "teacher " + user.UserName + " already exist");
        }
        public User getTeacherById(Guid teacherId)
        {
            return Uow.TeacherRepository.GetById(teacherId) ?? throw new NotImplementedException(message: "teacher with id " + teacherId + " not found!");
        }
        public IEnumerable<User> getAllTeachers()
        {
            return Uow.TeacherRepository.GetAll().Where(t => t.Type != UserTypeEnum.Admin);
        }

        public IEnumerable<User> getAllLabTeachers()
        {
            return Uow.TeacherRepository.Find(t => t.Type == UserTypeEnum.LapTeacher);
        }

        public IEnumerable<User> getAllTheoryTeachers()
        {
            return Uow.TeacherRepository.Find(t => t.Type == UserTypeEnum.Professor || t.Type == UserTypeEnum.DepartmentHead);
        }
    }
}

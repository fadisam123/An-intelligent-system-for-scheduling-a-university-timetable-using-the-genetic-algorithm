using Microsoft.AspNetCore.Identity;
using Timetable.Application.Persistence.UnitOfWork;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Application.Services.DataIO.Teacher
{
    public class TeacherService : ITeacherService
    {
        private IUnitOfWork Uow { get; }
        private UserManager<User> UserManager { get; }
        private RoleManager<Role> RoleManager { get; }
        public TeacherService(IUnitOfWork uow, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            Uow = uow;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public async Task<User> createTeacherAsync(string name, UserTypeEnum teacherType, string userName, string password)
        {
            if (await UserManager.FindByNameAsync(userName) is null)
            {
                User user = new User { Name = name, Type = teacherType, UserName = userName, Email = userName + "@users.com" };
                var userResult = await UserManager.CreateAsync(user, password);
                if (userResult.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, teacherType.ToString());
                    return user;
                }
                else
                {
                    throw new NotImplementedException(message: "error in creating the teacher");
                }
            }
            throw new NotImplementedException(message: "teacher " + userName + " already exist");
        }
        public User getTeacherById(Guid teacherId)
        {
            return Uow.Teachers.GetById(teacherId) ?? throw new NotImplementedException(message: "teacher with id " + teacherId + " not found!");
        }
        public IEnumerable<User> getAllTeachers()
        {
            return Uow.Teachers.GetAll().Where(t => t.Type != UserTypeEnum.Admin);
        }
    }
}

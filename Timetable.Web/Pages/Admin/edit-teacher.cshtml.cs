using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timetable.Application.Services.DataIO.Teacher;
using Timetable.RazorWeb.ViewModels;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class edit_teacherModel : PageModel
    {
        #region Fields
        private readonly ITeacherService _teacherServices;
        private readonly IValidator<TeacherViewModel> _validator;
        #endregion

        #region Input Data
        [BindProperty]
        public TeacherViewModel teachersViewModel { get; set; } = null!;
        #endregion

        #region Output Data
        public List<UserTypeEnum> TeacherTypes { get; set; } = new List<UserTypeEnum>();
        #endregion

        #region Constructor
        public edit_teacherModel(ITeacherService teacherServices, IValidator<TeacherViewModel> validator)
        {
            _validator = validator;
            _teacherServices = teacherServices;
        }
        #endregion
        public async Task OnGet(string teacherId)
        {
            Guid TeacherId;
            if (!Guid.TryParse(teacherId, out TeacherId))
            {
                throw new NotImplementedException(message: teacherId + " is not a valid guid");
            }
            var Teacher = _teacherServices.getTeacherById(TeacherId);
            teachersViewModel = new TeacherViewModel { ID = teacherId, Name = Teacher.Name, SelectedTeacherType = Teacher.Type, UserName = Teacher.UserName };

            foreach (UserTypeEnum type in Enum.GetValues(typeof(UserTypeEnum)))
            {
                if (type != UserTypeEnum.Admin)
                {
                    TeacherTypes.Add(type);
                }
            }
        }

        public async Task OnPost(string teacherId)
        {
            Guid TeacherId;
            if (!Guid.TryParse(teacherId, out TeacherId))
            {
                throw new NotImplementedException(message: teacherId + " is not a valid guid");
            }

            
        }
    }
}

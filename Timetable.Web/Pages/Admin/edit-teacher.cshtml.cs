using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timetable.Application.Services.DataIO.Teacher;
using Timetable.RazorWeb.ViewModels.InputModels;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class edit_teacherModel : PageModel
    {
        #region Fields
        private readonly ITeacherService _teacherService;
        private readonly IValidator<TeacherInputModel> _validator;
        #endregion

        #region Input Data
        [BindProperty]
        public TeacherInputModel teachersViewModel { get; set; } = null!;
        #endregion

        #region Output Data
        public List<UserTypeEnum> TeacherTypes { get; set; } = new List<UserTypeEnum>();
        #endregion

        #region Constructor
        public edit_teacherModel(ITeacherService teacherService, IValidator<TeacherInputModel> validator)
        {
            _validator = validator;
            _teacherService = teacherService;
        }
        #endregion
        public async Task OnGet(string teacherId)
        {
            Guid TeacherId;
            if (!Guid.TryParse(teacherId, out TeacherId))
            {
                throw new NotImplementedException(message: teacherId + " is not a valid guid");
            }
            var Teacher = _teacherService.getTeacherById(TeacherId);
            teachersViewModel = new TeacherInputModel { ID = teacherId, Name = Teacher.Name, SelectedTeacherType = Teacher.Type, UserName = Teacher.UserName };

            foreach (UserTypeEnum type in Enum.GetValues(typeof(UserTypeEnum)))
            {
                if (type != UserTypeEnum.Admin)
                {
                    TeacherTypes.Add(type);
                }
            }
        }

        public async Task<IActionResult> OnPost(string teacherId)
        {
            Guid TeacherId;
            if (!Guid.TryParse(teacherId, out TeacherId))
            {
                throw new NotImplementedException(message: teacherId + " is not a valid guid");
            }
            User teacher = _teacherService.getTeacherById(TeacherId);
            teacher.Name = teachersViewModel.Name;
            teacher.Type = teachersViewModel.SelectedTeacherType;
            if (teachersViewModel.password is not null)
            {
                await _teacherService.UpdateTeacher(teacher, teachersViewModel.password);
            }
            else
            {
                _teacherService.UpdateTeacher(teacher);
            }
            return RedirectToPage("./teachers");
        }
    }
}

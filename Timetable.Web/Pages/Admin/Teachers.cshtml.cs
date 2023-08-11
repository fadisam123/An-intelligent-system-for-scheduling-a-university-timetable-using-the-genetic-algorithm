using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using Timetable.Application.Services.DataIO.Teacher;
using Timetable.RazorWeb.ViewModels.InputModels;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class TeachersModel : PageModel
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
        public List<User> Teachers { get; set; } = new List<User>();
        #endregion

        #region Constructor
        public TeachersModel(ITeacherService teacherService, IValidator<TeacherInputModel> validator)
        {
            _validator = validator;
            this._teacherService = teacherService;
        }
        #endregion

        #region Handler methods
        public async Task OnGet()
        {
            teachersViewModel = new TeacherInputModel();

            Teachers = _teacherService.getAllTeachers().ToList();
            foreach (UserTypeEnum type in Enum.GetValues(typeof(UserTypeEnum)))
            {
                if (type != UserTypeEnum.Admin)
                {
                    TeacherTypes.Add(type);
                }
            }
        }

        public async Task<IActionResult> OnPost()
        {
            ValidationResult ValidationResult = await _validator.ValidateAsync(teachersViewModel);

            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(this.ModelState, "teachersViewModel");
                await OnGet();
                return Page();
            }

            User user = new User { Name = teachersViewModel.Name,
                Type = teachersViewModel.SelectedTeacherType,
                UserName = teachersViewModel.UserName,
                Email = teachersViewModel.UserName + "@users.com" };

            await _teacherService.createTeacherAsync(user, teachersViewModel.password);
            return RedirectToPage();

        }

        public async Task<IActionResult> OnPostDeleteAsync(string teacherId)
        {

            return RedirectToPage();
        } 
        #endregion
    }

}

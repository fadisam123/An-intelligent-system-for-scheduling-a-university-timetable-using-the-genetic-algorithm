using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timetable.Application.Services.DataIO.Teacher;
using Timetable.RazorWeb.ViewModels;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class TeachersModel : PageModel
    {
        #region Fields
        private readonly ITeacherService _tuc;
        private readonly IValidator<TeacherViewModel> _validator; 
        #endregion

        #region Input Data
        [BindProperty]
        public TeacherViewModel teachersViewModel { get; set; } = null!;
        #endregion

        #region Output Data
        public List<UserTypeEnum> TeacherTypes { get; set; } = new List<UserTypeEnum>();
        public List<User> Teachers { get; set; } = new List<User>();
        #endregion

        #region Constructor
        public TeachersModel(ITeacherService tuc, IValidator<TeacherViewModel> validator)
        {
            _validator = validator;
            this._tuc = tuc;
        }
        #endregion

        #region Handler methods
        public async Task OnGet()
        {
            ModelState.Clear();
            teachersViewModel = new TeacherViewModel();

            Teachers = _tuc.getAllTeachers().ToList();
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
                return RedirectToPage();
            }

            var userResult = await _tuc.createTeacherAsync(teachersViewModel.Name, teachersViewModel.SelectedTeacherType, teachersViewModel.UserName, teachersViewModel.password);
            return RedirectToPage();

        }

        public async Task<IActionResult> OnPostDeleteAsync(string teacherId)
        {

            return RedirectToPage();
        } 
        #endregion
    }

}

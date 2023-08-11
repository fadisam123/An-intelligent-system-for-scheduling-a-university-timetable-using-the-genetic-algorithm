using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timetable.Application.Services.DataIO.Course;
using Timetable.RazorWeb.ViewModels.InputModels;
using Timetable.RazorWeb.ViewModels.OutputModels;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class coursesModel : PageModel
    {
        #region Fields
        private readonly ICourseService _courseService;
        private readonly IValidator<CourseInputModel> _validator;
        #endregion

        #region Input Data
        [BindProperty]
        public CourseInputModel courseInputModel { get; set; } = new CourseInputModel()!;
        #endregion

        #region Output Data
        public CourseOutputModel courseOutputModel { get; set; } = new CourseOutputModel();
        #endregion

        public coursesModel(ICourseService courseService, IValidator<CourseInputModel> validator)
        {
            _courseService = courseService;
            _validator = validator;
        }
        public async Task OnGet()
        {
            courseOutputModel = new CourseOutputModel();
            foreach (var theoryCourse in _courseService.getAllTheoryCourses())
            {
                CourseView courseView = new CourseView {
                    Id = theoryCourse.Id.ToString(),
                    Name = theoryCourse.Name,
                    IsElective = theoryCourse.IsElective,
                    LuctureNumPerWeek = theoryCourse.LuctureNumPerWeek,
                    HasPracticalSection = _courseService.HasPracticalSection(theoryCourse),
                    Year = theoryCourse.year.YearNo,
                    Semester = theoryCourse.semester.SemesterNo,
                };
                courseOutputModel.Courses.Add(courseView);
            }
            courseOutputModel.Years = _courseService.getAllYears().ToList();
            courseOutputModel.Semesters = _courseService.getAllSemesters().ToList();
        }
        
        public async Task<IActionResult> OnPost()
        {
            ValidationResult validationResult = await _validator.ValidateAsync(courseInputModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState, "courseInputModel");
                await OnGet();
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string courseId)
        {

            return RedirectToPage();
        }
    }
}

using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Domain.Entities;
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

            Year? selectedYear = _courseService.getYear(courseInputModel.SelectedYear);
            Semester? selectedSemester = _courseService.getSemester(courseInputModel.SelectedSemester);

            Course course = new Course { Name = courseInputModel.Name,
                year = selectedYear,
                semester = selectedSemester,
                IsElective = courseInputModel.IsElective,
                Type = CourseTypeEnum.TheoryCourse,
                LuctureNumPerWeek = courseInputModel.LuctureNumPerWeek,
            };
            await _courseService.createCourseAsync(course);

            if (courseInputModel.HasPracticalSection)
            {
                Course labcourse = new Course
                {
                    Name = courseInputModel.Name,
                    year = selectedYear,
                    semester = selectedSemester,
                    IsElective = courseInputModel.IsElective,
                    Type = CourseTypeEnum.LapCourse,
                    LuctureNumPerWeek = courseInputModel.LuctureNumPerWeek
                };
                await _courseService.createCourseAsync(labcourse);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string courseId)
        {
            try
            {
                _courseService.DeleteCourseById(new Guid(courseId));
            }
            catch (DbUpdateException ex)
            {
                TempData["ExceptionMessage"] = "لا يمكن حذف هذا العنصر لأن بيانات أخرى مرتبطة معه إذا كنت تريد حذف هذا المقرر بالفعل قم بحذف كل البيانات المرتبطة معه في برنامج الدوام وبعد ذلك قم بحذفه";
                return RedirectToPage("/Error");
            }
            catch (Exception e)
            {
                TempData["ExceptionMessage"] = "حدث خطأ ما الرجاء المحاولة لاحقاً";
                return RedirectToPage("/Error");
            }
            return RedirectToPage();
        }
    }
}

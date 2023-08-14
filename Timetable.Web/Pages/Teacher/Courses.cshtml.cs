using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timetable.Application.Services.DataIO.Course;
using Timetable.RazorWeb.ViewModels.InputModels;
using Timetable.RazorWeb.ViewModels.OutputModels;

namespace Timetable.RazorWeb.Pages.Teacher
{
    public class CoursesModel : PageModel
    {
        #region Fields
        private readonly ICourseService _courseService;
        #endregion

        #region Output Data
        public CourseOutputModel courseOutputModel { get; set; } = new CourseOutputModel();
        #endregion

        public CoursesModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task OnGet()
        {
            var user = User.Identity;
            courseOutputModel = new CourseOutputModel();
            foreach (var theoryCourse in _courseService.getAllTheoryCourses())
            {
                CourseView courseView = new CourseView
                {
                    Id = theoryCourse.Id.ToString(),
                    Name = theoryCourse.Name,
                    IsElective = theoryCourse.IsElective,
                    LuctureNumPerWeek = theoryCourse.LuctureNumPerWeek,
                    HasPracticalSection = _courseService.HasPracticalSection(theoryCourse),
                    Year = theoryCourse.year.YearNo,
                    Semester = theoryCourse.semester.SemesterNo,
                    Type = theoryCourse.Type
                };
                courseOutputModel.Courses.Add(courseView);
            }
            courseOutputModel.Years = _courseService.getAllYears().ToList();
            courseOutputModel.Semesters = _courseService.getAllSemesters().ToList();
        }
    }
}

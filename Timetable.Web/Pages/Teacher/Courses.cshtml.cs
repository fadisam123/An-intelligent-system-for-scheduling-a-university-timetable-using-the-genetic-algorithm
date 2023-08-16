using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using Timetable.Application.Services.DataIO.Course;
using Timetable.RazorWeb.ViewModels.InputModels;
using Timetable.RazorWeb.ViewModels.OutputModels;

namespace Timetable.RazorWeb.Pages.Teacher
{
    public class CoursesModel : PageModel
    {
        #region Fields
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Input Data
        [DisplayName("عرض مقررات الفصل")]
        [BindProperty]
        public int SelectedSemesterInput { get; set; } = 1;
        #endregion

        #region Output Data
        public CourseOutputModel courseOutputModel { get; set; } = new CourseOutputModel();
        #endregion

        public CoursesModel(ICourseService courseService, UserManager<User> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }
        public async Task OnGet()
        {
            courseOutputModel = new CourseOutputModel();
            var user = await _userManager.GetUserAsync(User);
            Semester semester = _courseService.getSemester(SelectedSemesterInput);
            if (semester == null)
            {
                return;
            }
            foreach (var Course in _courseService.getAllTeacherSemesterCourses(user, semester).ToList())
            {
                CourseView courseView = new CourseView
                {
                    Id = Course.Id.ToString(),
                    Name = Course.Name,
                    IsElective = Course.IsElective,
                    LuctureNumPerWeek = Course.LuctureNumPerWeek,
                    Year = Course.year.YearNo,
                    Semester = Course.semester.SemesterNo,
                    Type = Course.Type,
                };
                courseOutputModel.Courses.Add(courseView);
            }
            courseOutputModel.Semesters = _courseService.getAllSemesters().ToList();
        }

        public async Task OnPost()
        {
            await OnGet();
        }
    }
}

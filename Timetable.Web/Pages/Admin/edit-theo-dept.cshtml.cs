using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Application.Services.DataIO.Teacher;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class edit_theo_deptModel : PageModel
    {
        #region Fields
        private readonly ITeacherService _teacherService;
        private readonly ICourseService _courseService;
        #endregion

        #region Input Data
        [BindProperty]
        [Required(ErrorMessage = "الرجاء اختيار مدرس")]
        [DisplayName("المدرس")]
        public Guid TeacherId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "الرجاء اختيار مقرر")]
        [DisplayName("المقرر الدراسي (نظري)")]
        public Guid CourseId { get; set; }
        #endregion

        #region Output Data
        public Course course { get; set; } = null!;
        public List<User> TheoryTeachers { get; set; } = new List<User>();
        #endregion

        public edit_theo_deptModel(ITeacherService teacherService, ICourseService courseService)
        {
            _teacherService = teacherService;
            _courseService = courseService;
        }
        public void OnGet(string theoryCourseId)
        {
            course = _courseService.getCourseById(new Guid(theoryCourseId));
            TeacherId = course.user.Id;
            CourseId = course.Id;
            TheoryTeachers = _teacherService.getAllTheoryTeachers().ToList();
        }

        public IActionResult OnPost(string theoryCourseId)
        {
            if (!ModelState.IsValid)
            {
                OnGet(theoryCourseId);
                return Page();
            }

            _courseService.AssignTheoryCourseToTeacherAsync(
                _courseService.getCourseById(CourseId),
                _teacherService.getTeacherById(TeacherId)
                );
            return RedirectToPage("./theo-dept");
        }
    }
}

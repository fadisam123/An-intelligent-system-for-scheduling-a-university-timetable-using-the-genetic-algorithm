using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Application.Services.DataIO.Teacher;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class theoreticalModel : PageModel
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
        [DisplayName("المقرر الدراسي")]
        public Guid CourseId { get; set; }
        #endregion

        #region Output Data
        public List<Course> TheoryCourses { get; set; } = new List<Course>();
        public List<User> TheoryTeachers { get; set; } = new List<User>();
        #endregion

        public theoreticalModel(ITeacherService teacherService, ICourseService courseService, IRoomService RoomService)
        {
            _teacherService = teacherService;
            _courseService = courseService;
        }
        public void OnGet()
        {
            TheoryTeachers = _teacherService.getAllTheoryTeachers().ToList();
            TheoryCourses = _courseService.getAllTheoryCourses().ToList();


        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            _courseService.AssignTheoryCourseToTeacherAsync(
                _courseService.getCourseById(CourseId),
                _teacherService.getTeacherById(TeacherId)
                );
            return RedirectToPage();
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        [DisplayName("المقرر الدراسي (نظري)")]
        public Guid CourseId { get; set; }
        #endregion

        #region Output Data
        public List<Course> AssignedTheoryCourses { get; set; } = new List<Course>();
        public List<Course> NotAssignedTheoryCourses { get; set; } = new List<Course>();
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

            AssignedTheoryCourses = _courseService.getAllAssignedTheoryCourses().ToList();
            NotAssignedTheoryCourses = _courseService.getAllNotAssignedTheoryCourses().OrderBy(c => c.semester.SemesterNo).ThenBy(c => c.year.YearNo).ToList();
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

        public async Task<IActionResult> OnPostDeleteAsync(string theoryCourseId)
        {
            ModelState.Clear();
            try
            {
                await _courseService.RemoveAssignedTheoryCourseTeacher(new Guid(theoryCourseId));
            }
            catch (DbUpdateException ex)
            {
                TempData["ExceptionMessage"] = "لا يمكن حذف هذا العنصر لأن بيانات أخرى مرتبطة معه";
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


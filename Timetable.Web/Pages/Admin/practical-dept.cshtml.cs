using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Application.Services.DataIO.Teacher;
using Timetable.RazorWeb.ViewModels.InputModels;
using Timetable.RazorWeb.ViewModels.OutputModels;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class practicalModel : PageModel
    {
        #region Fields
        private readonly ITeacherService _teacherService;
        private readonly ICourseService _courseService;
        private readonly IRoomService _roomService;
        #endregion

        #region Input Data
        [BindProperty]
        [Required(ErrorMessage = "الرجاء اختيار مدرس")]
        [DisplayName("المدرس")]
        public Guid TeacherId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "الرجاء اختيار مخبر")]
        [DisplayName("المخبر")]
        public Guid RoomId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "الرجاء اختيار مقرر")]
        [DisplayName("المقرر الدراسي (عملي)")]
        public Guid CourseId { get; set; }
        #endregion

        #region Output Data
        public List<Course> AssignedLabCourses { get; set; } = new List<Course>();
        public List<Course> NotAssignedLabCourses { get; set; } = new List<Course>();
        public List<User> LabTeachers { get; set; } = new List<User>();
        public List<Room> LabRooms { get; set; } = new List<Room>();
        #endregion

        public practicalModel(ITeacherService teacherService, ICourseService courseService, IRoomService RoomService)
        {
            _teacherService = teacherService;
            _courseService = courseService;
            _roomService = RoomService;
        }
        public void OnGet()
        {
            LabTeachers = _teacherService.getAllLabTeachers().ToList();
            AssignedLabCourses = _courseService.getAllAssignedLabCourses().ToList();
            NotAssignedLabCourses = _courseService.getAllNotAssignedLabCourses().OrderBy(c => c.semester.SemesterNo).ThenBy(c => c.year.YearNo).ToList();
            LabRooms = _roomService.getAllLabRooms().ToList();


        }

        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            _courseService.AssignLabCourseToTeacherAsync(
                _courseService.getCourseById(CourseId),
                _teacherService.getTeacherById(TeacherId),
                _roomService.getRoomById(RoomId)
                );
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string lapCourseId)
        {
            ModelState.Clear();
            try
            {
                await _courseService.RemoveAssignedLapCourseTeacher(new Guid(lapCourseId));
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

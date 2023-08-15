using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Application.Services.DataIO.Teacher;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class edit_practical_deptModel : PageModel
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
        public Course course { get; set; } = null!;
        public List<User> LabTeachers { get; set; } = new List<User>();
        public List<Room> LabRooms { get; set; } = new List<Room>();
        #endregion

        public edit_practical_deptModel(ITeacherService teacherService, ICourseService courseService, IRoomService RoomService)
        {
            _teacherService = teacherService;
            _courseService = courseService;
            _roomService = RoomService;
        }
        public void OnGet(string lapCourseId)
        {
            course = _courseService.getCourseById(new Guid(lapCourseId));
            RoomId = course.TeacherpreferredRoom.Id;
            TeacherId = course.user.Id;
            CourseId = course.Id;
            LabTeachers = _teacherService.getAllLabTeachers().ToList();
            LabRooms = _roomService.getAllLabRooms().ToList();
        }

        public IActionResult OnPost(string lapCourseId)
        {
            if (!ModelState.IsValid)
            {
                OnGet(lapCourseId);
                return Page();
            }

            _courseService.AssignLabCourseToTeacherAsync(
                _courseService.getCourseById(CourseId),
                _teacherService.getTeacherById(TeacherId),
                _roomService.getRoomById(RoomId)
                );
            return RedirectToPage("./practical-dept");
        }
    }
}

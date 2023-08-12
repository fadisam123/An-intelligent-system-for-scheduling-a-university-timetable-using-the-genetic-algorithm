using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        [DisplayName("المقرر الدراسي")]
        public Guid CourseId { get; set; }
        #endregion

        #region Output Data
        public List<Course> LabCourses { get; set; } = new List<Course>();
        public List<User> LabTeachers { get; set; } = new List<User>();
        public List<Room> LabRooms { get; set; } = new List<Room>();

        public List<PracticalDeptOutputModel> practicalDeptOutputModels { get; set; } = new List<PracticalDeptOutputModel>();
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
            LabCourses = _courseService.getAllLabCourses().ToList();
            LabRooms = _roomService.getAllLabRooms().ToList();


        }

        public IActionResult OnPost()
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
    }
}

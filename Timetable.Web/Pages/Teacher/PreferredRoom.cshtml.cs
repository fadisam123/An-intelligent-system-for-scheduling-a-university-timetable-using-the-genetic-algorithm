using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Domain.Enums;
using Timetable.RazorWeb.Authorization;

namespace Timetable.RazorWeb.Pages.Teacher
{
    [CustomAuthorize(new RoleEnum[] { RoleEnum.Professor, RoleEnum.DepartmentHead })]
    public class PreferredRoomModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userManager;

        public List<Room> rooms { set; get; } = null!;
        public List<Course> courses { set; get; } = null!;
        public List<Semester> semesters { set; get; } = null!;

        #region Input Data
        [DisplayName("الفصل الدراسي")]
        [BindProperty]
        public int SelectedSemesterInput { get; set; } = 1;

        [BindProperty]
        [Required(ErrorMessage = "الرجاء اختيار مقرر")]
        [DisplayName("المقرر الدراسي")]
        public Guid CourseId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "الرجاء اختيار قاعة")]
        [DisplayName("القاعة الدراسية المفضلة")]
        public Guid RoomId { get; set; }
        #endregion

        public PreferredRoomModel(IRoomService roomService, ICourseService courseService, UserManager<User> userManager)
        {
            _roomService = roomService;
            _courseService = courseService;
            _userManager = userManager;
        }
        public async Task OnGet()
        {
            rooms = _roomService.getAllTheoryRooms().ToList();
            semesters = _courseService.getAllSemesters().ToList();
            var user = await _userManager.GetUserAsync(User);
            Semester semester = _courseService.getSemester(SelectedSemesterInput);
            courses = _courseService.getAllTeacherSemesterCourses(user, semester).ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await OnGet();
                return Page();
            }

            Course course = _courseService.getCourseById(CourseId);
            Room room = _roomService.getRoomById(RoomId);

            _courseService.AssignTeacherPreferredRoom(course, room);
            return RedirectToPage();
        }

        public async Task OnPostX()
        {
            ModelState.Clear();
            await OnGet();
        }

        public async Task OnPostDelete(string courseId)
        {
            Course course = _courseService.getCourseById(new Guid(courseId));
            _ = course.TeacherpreferredRoom;
            course.TeacherpreferredRoom = null;
            _courseService.Update(course);
            ModelState.Clear();
            await OnGet();
        }
    }
}

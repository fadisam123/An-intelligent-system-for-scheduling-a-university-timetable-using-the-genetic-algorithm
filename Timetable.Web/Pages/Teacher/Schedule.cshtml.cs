using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Application.Services.DataIO.Teacher;

namespace Timetable.RazorWeb.Pages.Teacher
{
    public class ScheduleModel : PageModel
    {
        private readonly ILectureService _lectureService;
        private readonly ICourseService _courseService;
        private readonly IDayTimeService _dayTimeService;
        private readonly IRoomService _roomService;
        private readonly ITeacherService _teacherService;
        private readonly UserManager<User> _userManager;

        public List<Lecture> Lectures { get; set; } = null!;
        public List<Semester> Semesters { set; get; } = null!;
        public List<Day> Days { set; get; } = null!;
        public List<Time> Times { set; get; } = null!;

        [DisplayName("الفصل")]
        [BindProperty]
        public int SelectedSemester { get; set; } = 1;

        [DisplayName("عرض برنامج الدوام من أجل السنة:")]
        [BindProperty]
        public int SelectedYearInput { get; set; } = 1;
        [DisplayName("عرض برنامج الدوام من أجل الفصل")]
        [BindProperty]
        public int SelectedSemesterInput { get; set; } = 1;

        public ScheduleModel(ILectureService lectureService, ICourseService courseService, IDayTimeService dayTimeService, IRoomService roomService, ITeacherService teacherService, UserManager<User> userManager)
        {
            _lectureService = lectureService;
            _courseService = courseService;
            _dayTimeService = dayTimeService;
            _roomService = roomService;
            _teacherService = teacherService;
            _userManager = userManager;
        }
        public async Task OnGet()
        {
            User teacher = await _userManager.GetUserAsync(User);

            Semester semester = _courseService.getSemester(SelectedSemesterInput);

            if (!_courseService.CheckCoursesExistWithAssignedUsers())
            {
                return;
            }
            Semesters = _courseService.getAllSemesters().ToList();
            Days = _dayTimeService.GetAllDays().OrderByDescending(d => d.DayNo).ToList();
            Times = _dayTimeService.GetAllTimes().OrderBy(t => t.Start).ToList();

            Lectures = _lectureService.GetScheduleOfTeacher(teacher, semester).OrderByDescending(l => l.day.DayNo).ThenByDescending(l => l.Time.Start).ToList();
        }

        public async Task OnPostX()
        {
            await OnGet();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Application.Services.DataIO.Teacher;
using Timetable.Domain.Enums;
using Timetable.RazorWeb.Authorization;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class scheduleModel : PageModel
    {
        private readonly ILectureService _lectureService;
        private readonly ICourseService _courseService;
        private readonly IDayTimeService _dayTimeService;
        private readonly IRoomService _roomService;
        private readonly ITeacherService _teacherService;

        public List<Course> TheoryCourses { get; set; } = null!;
        public List<Room> TheoryRooms { get; set; } = null!;
        public List<User> TheoryTeachers { get; set; } = null!;

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


        public List<Lecture> Lectures { get; set; } = null!;
        public List<Semester> Semesters { set; get; } = null!;
        public List<Year> Years { set; get; } = null!;
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

        public scheduleModel(ILectureService lectureService, ICourseService courseService, IDayTimeService dayTimeService, IRoomService roomService, ITeacherService teacherService)
        {
            _lectureService = lectureService;
            _courseService = courseService;
            _dayTimeService = dayTimeService;
            _roomService = roomService;
            _teacherService = teacherService;
        }
        public async Task OnGet()
        {
            Semester semester = _courseService.getSemester(SelectedSemesterInput);
            Year year = _courseService.getYear(SelectedYearInput);

            TheoryCourses = _courseService.getAllTheoryCourses(semester, year).ToList();
            TheoryRooms = _roomService.getAllTheoryRooms().ToList();
            TheoryTeachers = _teacherService.getAllTheoryTeachers().ToList();

            if (!_courseService.CheckCoursesExistWithAssignedUsers())
            {
                return;
            }
            Semesters = _courseService.getAllSemesters().ToList();
            Years = _courseService.getAllYears().ToList();
            Days = _dayTimeService.GetAllDays().OrderByDescending(d => d.DayNo).ToList();
            Times = _dayTimeService.GetAllTimes().OrderBy(t => t.Start).ToList();

            Lectures = _lectureService.GetTheorySchedule(semester, year).OrderByDescending(l => l.day.DayNo).ThenByDescending(l => l.Time.Start).ToList();

        }

        public async Task OnPostGenerate()
        {
            Semester semester = _courseService.getSemester(SelectedSemester);
            _lectureService.GenerateTheorySchedule(semester);
            await OnPostX();
        }

        public async Task OnPostX()
        {
            await OnGet();
        }



        public async Task OnPostCreate(string dayId, string timeId)
        {
            Lecture newLecture = new Lecture
            {
                course = _courseService.getCourseById(CourseId),
                day = _dayTimeService.GetDayById(int.Parse(dayId)),
                Room = _roomService.getRoomById(RoomId),
                Time = _dayTimeService.GetTimeById(new Guid(timeId)),
                Type = LectureTypeEnum.TheoryLecture
            };
            _lectureService.AddLecture(newLecture);
            await OnGet();
        }

        public async Task OnPostDelete(string luctureId)
        {
            _lectureService.DeleteLectureById(new Guid(luctureId));
            await OnGet();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Domain.Enums;
using Timetable.RazorWeb.Authorization;

namespace Timetable.RazorWeb.Pages.Admin
{
    [CustomAuthorize(new RoleEnum[] { RoleEnum.Admin })]
    public class scheduleModel : PageModel
    {
        private readonly ILectureService _lectureService;
        private readonly ICourseService _courseService;
        private readonly IDayTimeService _dayTimeService;

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

        public scheduleModel(ILectureService lectureService, ICourseService courseService, IDayTimeService dayTimeService)
        {
            _lectureService = lectureService;
            _courseService = courseService;
            _dayTimeService = dayTimeService;
        }
        public void OnGet()
        {
            if (!_courseService.CheckCoursesExistWithAssignedUsers())
            {
                return;
            }
            Semesters = _courseService.getAllSemesters().ToList();
            Years = _courseService.getAllYears().ToList();
            Days = _dayTimeService.GetAllDays().OrderByDescending(d => d.DayNo).ToList();
            Times = _dayTimeService.GetAllTimes().OrderBy(t => t.Start).ToList();


            Semester semester = _courseService.getSemester(SelectedSemesterInput);
            Year year = _courseService.getYear(SelectedYearInput);
            Lectures = _lectureService.GetTheorySchedule(semester, year).OrderByDescending(l => l.day.DayNo).ThenByDescending(l => l.Time.Start).ToList();

        }

        public void OnPost()
        {
            Semester semester = _courseService.getSemester(SelectedSemester);
            _lectureService.GenerateTheorySchedule(semester);
            OnPostX();
        }

        public void OnPostX()
        {
            OnGet();
        }
    }
}

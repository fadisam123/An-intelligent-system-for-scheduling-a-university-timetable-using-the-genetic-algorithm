using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Domain.Entities;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class scheduleModel : PageModel
    {
        private readonly ILectureService _lectureService;
        private readonly ICourseService _courseService;

        public List<Lecture> Lectures { get; set; } = new List<Lecture>();
        public List<Semester> Semesters { set; get; }
        public List<Year> Years { set; get; }

        [DisplayName("الفصل")]
        [BindProperty]
        public int SelectedSemester { get; set; } = 1;

        [DisplayName("عرض برنامج الدوام من أجل السنة:")]
        [BindProperty]
        public int SelectedYearInput { get; set; } = 1;
        [DisplayName("عرض برنامج الدوام من أجل الفصل")]
        [BindProperty]
        public int SelectedSemesterInput { get; set; } = 1;

        public scheduleModel(ILectureService lectureService, ICourseService courseService)
        {
            _lectureService = lectureService;
            _courseService = courseService;
        }
        public void OnGet()
        {
            if (!_courseService.CheckCoursesExistWithAssignedUsers())
            {
                return;
            }
            Semesters = _courseService.getAllSemesters().ToList();
            Years = _courseService.getAllYears().ToList();
        }

        public void OnPost()
        {
            Semester semester = _courseService.getSemester(SelectedSemester);
            _lectureService.GenerateTheorySchedule(semester);
            OnPostX();
        }

        public void OnPostX()
        {
            Semester semester = _courseService.getSemester(SelectedSemesterInput);
            Year year = _courseService.getYear(SelectedYearInput);
            Lectures =  _lectureService.GetTheorySchedule(semester, year).ToList();
            OnGet();
        }
    }
}

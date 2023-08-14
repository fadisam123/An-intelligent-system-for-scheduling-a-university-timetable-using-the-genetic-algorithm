using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Application.Services.DataIO.Survey;
using Timetable.Application.Services.DataIO.Teacher;

namespace Timetable.RazorWeb.Pages.Teacher
{
    public class SurveyModel : PageModel
    {
        private readonly ITeacherService _teacherService;
        private readonly IDayTimeService _dayTimeService;
        private readonly ISurveyService _surveyService;
        private readonly UserManager<User> _userManager;

        public List<Day> Days { set; get; } = null!;
        public List<Time> Times { set; get; } = null!;
        public List<TeacherPreferenceDayTime> TeacherPreferrences { set; get; } = null!;

        [DisplayName("الفصل")]
        [BindProperty]
        public int SelectedSemester { get; set; } = 1;

        [DisplayName("عرض برنامج الدوام من أجل السنة:")]
        [BindProperty]
        public int SelectedYearInput { get; set; } = 1;
        [DisplayName("عرض برنامج الدوام من أجل الفصل")]
        [BindProperty]
        public int SelectedSemesterInput { get; set; } = 1;

        public SurveyModel(ITeacherService teacherService, IDayTimeService dayTimeService, ISurveyService surveyService, UserManager<User> userManager)
        {
            _teacherService = teacherService;
            _dayTimeService = dayTimeService;
            _surveyService = surveyService;
            _userManager = userManager;
        }
        public async Task OnGet()
        {
            var teacher = await _userManager.GetUserAsync(User);
            TeacherPreferrences = teacher.Preferences.OrderByDescending(p => p.day.DayNo).ThenByDescending(p => p.time.Start).ToList();
            Days = _dayTimeService.GetAllDays().ToList();
            Times = _dayTimeService.GetAllTimes().ToList();
        }

        public void OnPost()
        {
            
        }
    }
}

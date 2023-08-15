using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Application.Services.DataIO.Survey;
using Timetable.Application.Services.DataIO.Teacher;
using Timetable.Domain.Entities;

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
        public List<TeacherPreferenceDayTime> allPreferences { set; get; } = null!;

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
            Days = _dayTimeService.GetAllDays().OrderByDescending(d => d.DayNo).ToList();
            Times = _dayTimeService.GetAllTimes().OrderBy(t => t.Start).ToList();
            allPreferences = _surveyService.GetAllPreferences().ToList();

            for (int i = 0; i < Times.Count; i++)
            {

                for (int j = 0; j < Days.Count; j++)
                {
                    

                }

            }
        }

        public async Task OnPostCreate(string dayId, string timeId)
        {
            //Lecture newLecture = new Lecture
            //{
            //    course = _courseService.getCourseById(CourseId),
            //    day = _dayTimeService.GetDayById(int.Parse(dayId)),
            //    Room = _roomService.getRoomById(RoomId),
            //    Time = _dayTimeService.GetTimeById(new Guid(timeId)),
            //    Type = LectureTypeEnum.TheoryLecture
            //};
            //_lectureService.AddLecture(newLecture);
            await OnGet();
        }

        public async Task OnPostDelete(string preferrenceId)
        {
            //_lectureService.DeleteLectureById(new Guid(luctureId));
            await OnGet();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Application.Services.DataIO.Teacher;
using Timetable.RazorWeb.ViewModels.OutputModels;
using Timetable.RazorWeb.ViewModels.InputModels;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class Day_TimeModel : PageModel
    {
        #region Fields
        private readonly IDayTimeService _dayTimeService;
        #endregion

        [BindProperty]
        public DayTimeInputModel dayTimeInputModel { get; set; }
        public DayTimeOutputModel dayTimeOutputModel { get; set; }

        public List<Day> workingDays { get; set; } = new List<Day>();
        public List<Time> workingTimes { get; set; } = new List<Time>();

        public Day_TimeModel(IDayTimeService dayTimeService)
        {
            _dayTimeService = dayTimeService;
        }
        public void OnGet()
        {
            dayTimeInputModel = new DayTimeInputModel();
            workingDays = _dayTimeService.GetAllDays().ToList();
            workingTimes = _dayTimeService.GetAllTimes().ToList();

            foreach (var day in workingDays)
            {
                dayTimeInputModel.workingDays[day.DayNo - 1] = true;
            }
        }
        public IActionResult OnPost()
        {
            if (dayTimeInputModel.LectureDuration == TimeSpan.Zero)
            {
                ModelState.AddModelError("dayTimeInputModel.LectureDuration", "مدة الحصة مطلوب");
            
            }
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            //for (int i = 1; i < dayTimeInputModel.workingDays.Length; i++)
            //{
            //    if (dayTimeInputModel.workingDays[i - 1])
            //    {
                    
            //    }
            //}
            return RedirectToPage();
        }
    }
}

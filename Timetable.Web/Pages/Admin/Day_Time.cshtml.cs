using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Application.Services.DataIO.Teacher;
using Timetable.RazorWeb.ViewModels.OutputModels;
using Timetable.RazorWeb.ViewModels.InputModels;
using Microsoft.EntityFrameworkCore;
using Timetable.Domain.Entities;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class Day_TimeModel : PageModel
    {
        #region Fields
        private readonly IDayTimeService _dayTimeService;
        #endregion

        [BindProperty]
        public DayTimeInputModel dayTimeInputModel { get; set; }

        public List<Day> workingDays { get; set; } = new List<Day>();
        public List<Time> workingTimes { get; set; } = new List<Time>();

        public Day_TimeModel(IDayTimeService dayTimeService)
        {
            _dayTimeService = dayTimeService;
        }
        public void OnGet()
        {
            dayTimeInputModel = new DayTimeInputModel();
            workingDays = _dayTimeService.GetAllDays().OrderByDescending(d => d.DayNo).ToList();
            workingTimes = _dayTimeService.GetAllTimes().OrderBy(t => t.Start).ToList();

            foreach (var day in workingDays)
            {
                if (day.DayNo == 7)
                    dayTimeInputModel.workingDays[5] = true;
                else
                    dayTimeInputModel.workingDays[day.DayNo - 1] = true;
            }
            dayTimeInputModel.LectureDuration = workingTimes[0].End - workingTimes[0].Start;
            dayTimeInputModel.BreakDuration = workingTimes[1].Start - workingTimes[0].End;
            dayTimeInputModel.NumOfLecturePerDay = workingTimes.Count;
            dayTimeInputModel.FirstLectureTime = workingTimes[0].Start.ToTimeSpan();
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

            List<Day> days = new List<Day>();
            List<Time> times = new List<Time>();


            for (int i = 0; i < dayTimeInputModel.workingDays.Length; i++)
            {
                if (dayTimeInputModel.workingDays[i])
                {
                    Day day;
                    switch (i)
                    {
                        case 0:
                            day = new Day { DayNo = i + 1, Name = "الأحد" };
                            days.Add(day);
                            break;
                        case 1:
                            day = new Day { DayNo = i + 1, Name = "الأثنين" };
                            days.Add(day);
                            break;
                        case 2:
                            day = new Day { DayNo = i + 1, Name = "الثلاثاء" };
                            days.Add(day);
                            break;
                        case 3:
                            day = new Day { DayNo = i + 1, Name = "الأربعاء" };
                            days.Add(day);
                            break;
                        case 4:
                            day = new Day { DayNo = i + 1, Name = "الخميس" };
                            days.Add(day);
                            break;
                        default:
                            day = new Day { DayNo = 7, Name = "السبت" };
                            days.Add(day);
                            break;
                    }

                }
            }

            TimeOnly firstLecTime = TimeOnly.FromTimeSpan(dayTimeInputModel.FirstLectureTime.Value);
            Time time = new Time { Start = firstLecTime, End = firstLecTime.Add(dayTimeInputModel.LectureDuration.Value) };
            times.Add(time);
            for (int i = 0; i < dayTimeInputModel.NumOfLecturePerDay-1; i++)
            {
                if(dayTimeInputModel.BreakDuration is null)
                    time = new Time { Start = times[i].End, End = times[i].End.Add(dayTimeInputModel.LectureDuration.Value) };
                else
                    time = new Time { Start = times[i].End.Add(dayTimeInputModel.BreakDuration.Value), End = times[i].End.Add(dayTimeInputModel.BreakDuration.Value).Add(dayTimeInputModel.LectureDuration.Value)};
                times.Add(time);
            }

            try
            {
                _dayTimeService.RemoveDays();
                _dayTimeService.RemoveTimes();

                _dayTimeService.AddDays(days.ToArray());
                _dayTimeService.AddTimes(times.ToArray());
            }
            catch (DbUpdateException ex)
            {
                TempData["ExceptionMessage"] = "لا يمكن تعديل هذه اليانات لأنه يوجد بيانات أخرى مرتبطة معها إذا كنت تريد تعديلها بالفعل قم بحذف كل المقررات الدراسية أولاً ومن ثم قم بتعديلها";
                return RedirectToPage("/Error");
            }
            catch (Exception e)
            {
                TempData["ExceptionMessage"] = "حدث خطأ ما الرجاء المحاولة لاحقاً";
                return RedirectToPage("/Error");
            }

            return RedirectToPage();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Application.Services.DataIO.SemesterYear;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class semester_yearModel : PageModel
    {
        private readonly ISemesterYearService _semesterYearService;

        #region Output Data
        public List<Semester> Semesters { set; get; } = null!;
        public List<Year> Years { set; get; } = null!;
        #endregion

        #region Input Data
        [DisplayName("عدد السنوات الدراسية في الجامعة")]
        [Range(1, 6, ErrorMessage = "عدد السنوات الدراسية يجب أن يكون بين 1 و 6 سنوات")]
        [BindProperty]
        public int NumOfYearInCollege { get; set; } = 1;
        [DisplayName("عدد الفصول الدراسية في السنة")]
        [Range(1, 2, ErrorMessage = "عدد الفصول الدراسية يجب أن يكون بين 1 و 3 فصول")]
        [BindProperty]
        public int NumOfSemesterInYear { get; set; } = 1;
        #endregion

        public semester_yearModel(ISemesterYearService semesterYearService)
        {
            _semesterYearService = semesterYearService;
        }
        public void OnGet()
        {
            Years = _semesterYearService.getAllYears().ToList();
            Semesters= _semesterYearService.getAllSemesters().ToList();
            NumOfYearInCollege = Years.Count;
            NumOfSemesterInYear = Semesters.Count;
        }

        public IActionResult OnPost()
        {
            return RedirectToPage();
        }
    }
}

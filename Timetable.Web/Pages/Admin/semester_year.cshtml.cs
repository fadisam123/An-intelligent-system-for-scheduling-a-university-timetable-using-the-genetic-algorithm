using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Application.Services.DataIO.SemesterYear;
using Timetable.Domain.Entities;

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
        [Required(ErrorMessage = "عدد السنوات مطلوب")]
        [Range(1, 6, ErrorMessage = "عدد السنوات الدراسية يجب أن يكون بين 1 و 6 سنوات")]
        [BindProperty]
        public int NumOfYearInCollege { get; set; } = 1;
        [DisplayName("عدد الفصول الدراسية في السنة")]
        [Required(ErrorMessage = "عدد الفصول مطلوب")]
        [Range(1, 3, ErrorMessage = "عدد الفصول الدراسية يجب أن يكون بين 1 و 3 فصول")]
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
            Semesters = _semesterYearService.getAllSemesters().ToList();
            NumOfYearInCollege = Years.Count;
            NumOfSemesterInYear = Semesters.Count;
        }

        public IActionResult OnPost()
        {
            Year[] years = new Year[NumOfYearInCollege];
            Semester[] semesters = new Semester[NumOfSemesterInYear];
            for (int i = 1; i <= NumOfYearInCollege; i++)
            {
                years[i-1] = new Year { YearNo = i };
            }
            for (int i = 1; i <= NumOfSemesterInYear; i++)
            {
                semesters[i-1] = new Semester { SemesterNo = i };
            }
            try
            {
                _semesterYearService.DeleteAllYears();
                _semesterYearService.DeleteAllSemesters();

                _semesterYearService.AddYears(years);
                _semesterYearService.AddSemesters(semesters);
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

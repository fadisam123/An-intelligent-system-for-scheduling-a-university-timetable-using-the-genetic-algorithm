using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Timetable.RazorWeb.ViewModels.InputModels
{
    public class CourseInputModel
    {
        public string? Id { get; set; }

        [DisplayName("اسم المقرر")]
        [Remote(action: "IsCourseNameInUse", controller: "RemoteValidators")]
        public string? Name { get; set; } = string.Empty;

        [DisplayName("عدد المحاضرات الأسبوعية النظرية")]
        public int LuctureNumPerWeek { get; set; } = 1;

        [DisplayName("عدد المحاضرات الأسبوعية العملية")]
        public int LapLuctureNumPerWeek { get; set; } = 1;

        [DisplayName("يوجد له قسم عملي")]
        public bool HasPracticalSection { get; set; } = false;

        [DisplayName("هل المادة اختيارية")]
        public bool IsElective { get; set; } = false;

        [DisplayName("السنة")]
        public Year? SelectedYear { get; set; }

        [DisplayName("الفصل")]
        public Semester? SelectedSemester { get; set; }

    }
}

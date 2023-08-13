using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Timetable.RazorWeb.Validators;

namespace Timetable.RazorWeb.ViewModels.InputModels
{
    public class DayTimeInputModel
    {
        public bool[] workingDays { get; set; } = new bool[6];

        [DisplayName("عدد الحصص (المحاضرات) في اليوم")]
        public int NumOfLecturePerDay { get; set; } = 1;

        [DisplayName("مدة الحصة (المحاضرة) HH:MM")]
        [Required(ErrorMessage = "مدة الحصة مطلوب")]
        [TimeSpanAttributeValidation]
        public TimeSpan? LectureDuration { get; set; } = null!;

        [DisplayName("مدة الاستراحة بين الحصتين (المحاضرتين) HH:MM")]
        [TimeSpanAttributeValidation]
        public TimeSpan? BreakDuration { get; set;} = null!;

        [DisplayName("توقيت بداية الدوام في اليوم (وقت بداية أول محاضرة)")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "وقت بداية أول محاضرة مطلوب")]
        public TimeSpan? FirstLectureTime { get; set;} = null!;

    }
}

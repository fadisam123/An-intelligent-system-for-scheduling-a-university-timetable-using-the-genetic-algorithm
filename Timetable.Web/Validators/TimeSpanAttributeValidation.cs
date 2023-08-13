using System.ComponentModel.DataAnnotations;

namespace Timetable.RazorWeb.Validators
{
    public class TimeSpanAttributeValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is TimeSpan timeSpan)
            {
                if (timeSpan.TotalHours >= 0 && timeSpan.TotalHours < 24)
                {
                    return true;
                }
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"القيمة المدخلة غير صالحة أدخل قيمة بتنسيق HH:MM";
        }


    }
}

using FluentValidation;
using Newtonsoft.Json.Linq;
using Timetable.RazorWeb.ViewModels.InputModels;

namespace Timetable.RazorWeb.Validators
{
    public class CourseInputModelValidator : AbstractValidator<CourseInputModel>
    {
        public CourseInputModelValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("الاسم مطلوب");
            RuleFor(x => x.LuctureNumPerWeek)
                .Must(enterdValue => int.TryParse(enterdValue.ToString(), out _))
                .WithMessage("عدد المحاضرات يجب أن يكون رقم صحيح")
                .GreaterThan(0).WithMessage("عدد المحاضرات يجب أن يكون أكبر من 0");
            RuleFor(x => x.LapLuctureNumPerWeek)
                .Must(enterdValue => int.TryParse(enterdValue.ToString(), out _))
                .WithMessage("عدد المحاضرات يجب أن يكون رقم صحيح")
                .GreaterThan(0).WithMessage("عدد المحاضرات يجب أن يكون أكبر من 0");

        }
    }
}

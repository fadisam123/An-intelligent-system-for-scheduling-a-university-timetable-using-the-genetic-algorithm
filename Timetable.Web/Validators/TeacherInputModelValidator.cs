using FluentValidation;
using Timetable.RazorWeb.ViewModels.InputModels;

namespace Timetable.RazorWeb.Validators
{
    public class TeacherInputModelValidator : AbstractValidator<TeacherInputModel>
    {
        public TeacherInputModelValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("الاسم مطلوب");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("اسم المستخدم مطلوب");
            RuleFor(x => x.password).NotEmpty().WithMessage("كلمة السر مطلوبة").MinimumLength(6).WithMessage("كلمة السر يجب أن تكون بطول 6 محارف");
        }
    }
}

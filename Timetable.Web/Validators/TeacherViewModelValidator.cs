using FluentValidation;
using Timetable.RazorWeb.ViewModels;

namespace Timetable.RazorWeb.Validators
{
    public class TeacherViewModelValidator : AbstractValidator<TeacherViewModel>
    {
        public TeacherViewModelValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("الاسم مطلوب");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("اسم المستخدم مطلوب");
            RuleFor(x => x.password).NotEmpty().WithMessage("كلمة السر مطلوبة").Length(6, 25).WithMessage("كلمة السر يجب أن تكون بطول 6 الى 25 محرف");
        }
    }
}

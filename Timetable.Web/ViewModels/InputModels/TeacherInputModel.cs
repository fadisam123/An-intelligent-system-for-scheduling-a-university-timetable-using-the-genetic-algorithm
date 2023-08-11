using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Timetable.RazorWeb.ViewModels.InputModels
{
    public class TeacherInputModel
    {
        public string? ID { get; set; }

        [DisplayName("الأسم")]
        [DataType(DataType.Text, ErrorMessage = "الاسم يجب أن يكون من نوع نصي")]
        public string? Name { get; set; }

        [DisplayName("اسم المستخدم")]
        [Remote(action: "IsUserNameInUse", controller: "RemoteValidators")]
        public string? UserName { get; set; }

        [DisplayName("كلمة السر")]
        [DataType(DataType.Password)]
        public string? password { get; set; }


        [DisplayName("الصفة")]
        public UserTypeEnum SelectedTeacherType { get; set; } = UserTypeEnum.Professor;
    }
}

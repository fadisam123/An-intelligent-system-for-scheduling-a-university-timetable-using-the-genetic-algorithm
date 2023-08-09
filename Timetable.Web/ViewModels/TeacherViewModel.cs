using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Timetable.RazorWeb.ViewModels
{
    public class TeacherViewModel
    {
        public string? ID { get; set; }
        [DisplayName("الأسم")]
        public string? Name { get; set; }
        [DisplayName("اسم المستخدم")]
        [Remote(action: "IsUserNameInUse", controller: "Teachers")]
        public string? UserName { get; set; }
        [DisplayName("كلمة السر")]
        [DataType(DataType.Password)]
        public string? password { get; set; }

        [DisplayName("الصفة")]
        public UserTypeEnum SelectedTeacherType { get; set; } = UserTypeEnum.Professor;
    }
}

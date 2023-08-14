using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Timetable.RazorWeb.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        [DataType(DataType.Text)]
        [DisplayName("اسم المستخدم")]
        public string? userName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "كلمة السر مطلوبة")]
        [DataType(DataType.Password)]
        [DisplayName("كلمة المرور")]
        public string? password { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Timetable.Domain.Entities;

namespace Timetable.RazorWeb.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

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

        [BindProperty]
        [DisplayName("(تذكرني) ابق في حالة تسجيل دخول حتى إذا تم اغلاق المتصفح")]
        public bool isPersistent { get; set; } = false;


        public LoginModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userName);
                var logInResult = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: isPersistent, lockoutOnFailure: false);

                    if (logInResult.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToPage("/Admin/schedule");
                    }
                    else
                    {
                        return RedirectToPage("/Teacher/Courses");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "محاولة تسجيل دخول غير ناجحة! إذا نسيت كلمة المرور أو بيانات حسابك راجع مكتب المدير لحل المشكلة");
                }
            }
            
            return Page();
        }
    }
}

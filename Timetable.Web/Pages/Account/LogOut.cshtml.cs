using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Timetable.RazorWeb.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;

        public LogoutModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //await _signInManager.SignOutAsync();

            //// Clear the existing authentication cookies
            //await HttpContext.SignOutAsync();

            //return RedirectToPage("/Account/Login");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();

            // Clear the existing authentication cookies
            await HttpContext.SignOutAsync();

            return RedirectToPage("/Account/Login");
        }
    }
}

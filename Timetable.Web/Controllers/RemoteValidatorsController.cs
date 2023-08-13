using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timetable.RazorWeb.ViewModels;

namespace Timetable.RazorWeb.Controllers
{
    public class RemoteValidatorsController : Controller
    {
        private readonly UserManager<User> userManager;

        public RemoteValidatorsController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [AcceptVerbs("get", "post")]
        public async Task<IActionResult> IsUserNameInUse()
        {
            string userName = HttpContext.Request.Query["teachersViewModel.userName"];

            var result = await userManager.FindByNameAsync(userName);
            if (result == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"اسم المستخدم {userName} محجوز");
            }
        }

        [AcceptVerbs("get", "post")]
        public async Task<IActionResult> IsRoomNameInUse()
        {
            string userName = HttpContext.Request.Query["teachersViewModel.userName"];

            var result = await userManager.FindByNameAsync(userName);
            if (result == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"اسم المستخدم {userName} محجوز");
            }
        }

        [AcceptVerbs("get", "post")]
        public async Task<IActionResult> IsCourseNameInUse()
        {
            string userName = HttpContext.Request.Query["teachersViewModel.userName"];

            var result = await userManager.FindByNameAsync(userName);
            if (result == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"اسم المستخدم {userName} محجوز");
            }
        }
    }
}

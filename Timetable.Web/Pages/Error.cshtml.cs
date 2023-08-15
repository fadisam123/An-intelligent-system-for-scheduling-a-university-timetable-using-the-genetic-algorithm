using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace Timetable.Web.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? ExceptionMessage { get; set; }

        public ErrorModel() { }

        public void OnGet()
        {
            ExceptionMessage = TempData["ExceptionMessage"]?.ToString();
            TempData.Remove("ExceptionMessage");
        }
    }
}
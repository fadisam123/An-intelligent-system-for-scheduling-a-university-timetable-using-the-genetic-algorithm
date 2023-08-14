using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Survey;
using Timetable.Domain.Enums;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class surveyModel : PageModel
    {
        private readonly ISurveyService _surveyService;
        private readonly RoleManager<Role> _roleManager;

        #region Output Data
        public List<RoleEnum> Roles = new List<RoleEnum>();
        public List<TakingSurveyAllowedPeriod> Surveys;
        #endregion

        #region InputData
        [DisplayName("استبيان من أجل")]
        [BindProperty]
        public RoleEnum SelectedRole { get; set; } = RoleEnum.DepartmentHead;

        [DisplayName("تاريخ فتح أو بدء الاستبيان")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "اضبط وقت بدء الاستبيان")]
        [BindProperty]
        public DateTime? StartDateTime { get; set; } = null!;

        [DisplayName("تاريخ إغلاق أو انتهاء الاستبيان")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "اضبط وقت انتهاء الاستبيان")]
        [BindProperty]
        public DateTime? EndDateTime { get; set; } = null!;
        #endregion


        public surveyModel(ISurveyService surveyService, RoleManager<Role> roleManager)
        {
            _surveyService = surveyService;
            _roleManager = roleManager;
        }


        public void OnGet()
        {
            foreach (RoleEnum type in Enum.GetValues(typeof(RoleEnum)))
            {
                if (type != RoleEnum.Admin)
                {
                    Roles.Add(type);
                }
            }
            Surveys = _surveyService.getAllSurveys().ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            if (EndDateTime <= StartDateTime)
            {
                ModelState.AddModelError("StartDateTime", "لايمكن ادخال توقيتين متطابقين ويجب أن يكون وقت الانتهاء بعد وقت البدء");
                ModelState.AddModelError("EndDateTime", "لايمكن ادخال توقيتين متطابقين ويجب أن يكون وقت الانتهاء بعد وقت البدء");
            }

            if (StartDateTime < DateTime.Now.AddMinutes(-5))
            {
                ModelState.AddModelError("StartDateTime", "لايمكن أن يكون وقت البداية في الماضي");
            }

            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }
            TakingSurveyAllowedPeriod survey = new TakingSurveyAllowedPeriod
            {
                Start = StartDateTime.Value,
                End = EndDateTime.Value,
                role = await _roleManager.FindByNameAsync(SelectedRole.ToString())
            };
            TakingSurveyAllowedPeriod? existSurvey = _surveyService.getSurveyByRole(SelectedRole);
            if (existSurvey is null)
            {
                await _surveyService.createSurveyAsync(survey);
            }
            else
            {
                existSurvey.Start = StartDateTime.Value;
                existSurvey.End = EndDateTime.Value;
                _surveyService.updateSurvey(existSurvey);
            }
            
            return RedirectToPage();
        }
    }
}

using Timetable.Domain.Enums;

namespace Timetable.Application.Services.DataIO.Survey
{
    public interface ISurveyService
    {
        public Task createSurveyAsync(TakingSurveyAllowedPeriod survey);
        public TakingSurveyAllowedPeriod getSurveyByRole(RoleEnum roleEnum);
    }
}

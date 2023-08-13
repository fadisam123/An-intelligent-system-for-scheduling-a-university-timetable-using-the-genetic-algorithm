using Timetable.Application.Persistence.UnitOfWork;
using Timetable.Domain.Enums;

namespace Timetable.Application.Services.DataIO.Survey
{
    public class SurveyService : ISurveyService
    {
        private IUnitOfWork Uow { get; }
        public SurveyService(IUnitOfWork uow)
        {
            Uow = uow;
        }
        public async Task createSurveyAsync(TakingSurveyAllowedPeriod survey)
        {
            Uow.SurveyRepository.Add(survey);
            Uow.SaveChanges();
        }

        public TakingSurveyAllowedPeriod getSurveyByRole(RoleEnum roleEnum)
        {
            throw new NotImplementedException();
        }
    }
}

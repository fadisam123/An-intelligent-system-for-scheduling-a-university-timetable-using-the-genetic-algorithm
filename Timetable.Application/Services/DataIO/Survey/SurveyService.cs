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

        public IEnumerable<TakingSurveyAllowedPeriod> getAllSurveys()
        {
            return Uow.SurveyRepository.GetAll();
        }

        public TakingSurveyAllowedPeriod? getSurveyByRole(RoleEnum roleEnum)
        {
            return Uow.SurveyRepository.Find(s => s.role.Name.ToUpper() == roleEnum.ToString().ToUpper()).FirstOrDefault();
        }

        public void updateSurvey(TakingSurveyAllowedPeriod survey)
        {
            Uow.SurveyRepository.Update(survey);
            Uow.SaveChanges();
        }
    }
}

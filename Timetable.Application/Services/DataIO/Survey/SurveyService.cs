using Microsoft.AspNetCore.Identity;
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

        public IEnumerable<TeacherPreferenceDayTime> GetAllPreferences()
        {
            return Uow.SurveyRepository.GetAllPreferences();
        }

        public void AddPreferrenceToTeacher(User teacher, Day day, Time time)
        {
            TeacherPreferenceDayTime tpdt = new TeacherPreferenceDayTime { 
                day = day,
                time = time,
                user = teacher
            };
            Uow.TeacherPreferenceDayTimesRepository.Add(tpdt);
            Uow.SaveChanges();
        }

        public void DeletePreferrence(TeacherPreferenceDayTime preferrence)
        {
            Uow.TeacherPreferenceDayTimesRepository.Remove(preferrence);
            Uow.SaveChanges();
        }

        public bool IsAllowedTakeingSurvay(User teacher)
        {
            TakingSurveyAllowedPeriod? allowedPeriod = Uow.SurveyRepository.GetAll().Where(s => s.role.Name.ToUpper() == teacher.Type.ToString().ToUpper()).FirstOrDefault();
            if (allowedPeriod is not null) 
            {
                if ( DateTime.Now >= allowedPeriod.Start && DateTime.Now <= allowedPeriod.End)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

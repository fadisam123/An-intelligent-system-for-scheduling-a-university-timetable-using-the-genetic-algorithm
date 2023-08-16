using Timetable.Domain.Enums;

namespace Timetable.Application.Services.DataIO.Survey
{
    public interface ISurveyService
    {
        public Task createSurveyAsync(TakingSurveyAllowedPeriod survey);
        public IEnumerable<TakingSurveyAllowedPeriod>  getAllSurveys();
        public TakingSurveyAllowedPeriod?  getSurveyByRole(RoleEnum roleEnum);
        public void  updateSurvey(TakingSurveyAllowedPeriod survey);
        public IEnumerable<TeacherPreferenceDayTime> GetAllPreferences();
        public void AddPreferrenceToTeacher(User teacher, Day day, Time time);
        public void DeletePreferrence(TeacherPreferenceDayTime preferrence);
        public bool IsAllowedTakeingSurvay(User teacher);
    }
}

using Timetable.Domain.Entities;

namespace Timetable.Application.Persistence.Repository
{
    public interface ISurveyRepository : IGenericRepository<TakingSurveyAllowedPeriod>
    {
        public IEnumerable<TeacherPreferenceDayTime> GetAllPreferences();
    }
}

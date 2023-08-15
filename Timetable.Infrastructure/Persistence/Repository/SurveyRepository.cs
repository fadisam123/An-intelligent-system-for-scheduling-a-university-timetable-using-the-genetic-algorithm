using Timetable.Application.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class SurveyRepository : GenericRepository<TakingSurveyAllowedPeriod>, ISurveyRepository
    {
        public SurveyRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<TeacherPreferenceDayTime> GetAllPreferences()
        {
            return _context.TeacherPreferenceDayTimes;
        }
    }
}

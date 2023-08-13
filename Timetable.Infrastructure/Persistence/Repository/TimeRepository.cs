using Timetable.Application.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class TimeRepository : GenericRepository<Time>, ITimeRepository
    {
        public TimeRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using Timetable.Application.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class YearRepository : GenericRepository<Year>, IYearRepository
    {
        public YearRepository(AppDbContext context) : base(context)
        {
        }
    }
}

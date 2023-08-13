using Timetable.Application.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class DayRepository : GenericRepository<Day>, IDayRepository
    {
        public DayRepository(AppDbContext context) : base(context)
        {
        }
    }
}

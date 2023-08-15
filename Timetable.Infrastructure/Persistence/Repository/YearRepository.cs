using Timetable.Application.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class YearRepository : GenericRepository<Year>, IYearRepository
    {
        public YearRepository(AppDbContext context) : base(context)
        {
        }
        public void Clear()
        {
            _context.Years.RemoveRange(_context.Years);
            _context.SaveChanges();
        }
    }
}

using Timetable.Application.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class SemesterRepository : GenericRepository<Semester>, ISemesterRepository
    {
        public SemesterRepository(AppDbContext context) : base(context)
        {
        }

        public void Clear()
        {
            _context.Semesters.RemoveRange(_context.Semesters);
            _context.SaveChanges();
        }
    }
}

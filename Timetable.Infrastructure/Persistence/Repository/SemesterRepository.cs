using Timetable.Application.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class SemesterRepository : GenericRepository<Semester>, ISemesterRepository
    {
        public SemesterRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using Timetable.Application.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class LectureRepository : GenericRepository<Lecture> , ILectureRepository
    {
        public LectureRepository(AppDbContext context) : base(context)
        {
        }
    }
}

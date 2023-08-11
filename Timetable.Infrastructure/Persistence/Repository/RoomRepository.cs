using Timetable.Application.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context)
        {
        }
    }
}

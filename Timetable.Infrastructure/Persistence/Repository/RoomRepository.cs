using Timetable.Application.Persistence.Repository;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Room> GetAllLabRoom()
        {
            return _context.Rooms.Where(r => r.type == RoomTypeEnum.LapRoom || r.type == RoomTypeEnum.MixedRoom);
        }

        public IEnumerable<Room> GetAllTheoryRoom()
        {
            return _context.Rooms.Where(r => r.type == RoomTypeEnum.TheoryRoom || r.type == RoomTypeEnum.MixedRoom);
        }
    }
}

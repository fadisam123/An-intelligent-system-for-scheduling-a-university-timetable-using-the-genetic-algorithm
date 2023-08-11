using Timetable.Application.Persistence.UnitOfWork;

namespace Timetable.Application.Services.DataIO.Room
{
    using Timetable.Domain.Entities;
    public class RoomService : IRoomService
    {
        private IUnitOfWork Uow { get; }
        public RoomService(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public async Task createRoomAsync(Room room)
        {
            Uow.RoomRepository.Add(room);
            Uow.SaveChanges();
        }

        public Room getRoomById(Guid roomId)
        {
            return Uow.RoomRepository.GetById(roomId);
        }

        public IEnumerable<Room> getAllRooms()
        {
            return Uow.RoomRepository.GetAll();
        }
    }
}

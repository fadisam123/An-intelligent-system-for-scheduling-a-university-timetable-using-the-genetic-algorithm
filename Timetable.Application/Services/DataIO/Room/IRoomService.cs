namespace Timetable.Application.Services.DataIO.Room
{
    using Timetable.Domain.Entities;
    public interface IRoomService
    {
        public Task createRoomAsync(Room room);
        public Room getRoomById(Guid roomId);
        public IEnumerable<Room> getAllRooms();
    }
}

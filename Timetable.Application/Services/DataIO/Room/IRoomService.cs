﻿namespace Timetable.Application.Services.DataIO.Room
{
    using Timetable.Domain.Entities;
    public interface IRoomService
    {
        public Task createRoomAsync(Room room);
        public Room getRoomById(Guid roomId);
        public IEnumerable<Room> getAllRooms();
        public IEnumerable<Room> getAllTheoryRooms();
        public IEnumerable<Room> getAllLabRooms();
        public void deleteRoomById(Guid roomId);
        public void UpdateRoom(Room room);
    }
}

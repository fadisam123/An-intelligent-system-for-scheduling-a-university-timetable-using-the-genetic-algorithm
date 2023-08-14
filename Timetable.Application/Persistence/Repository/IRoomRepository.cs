namespace Timetable.Application.Persistence.Repository
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        IEnumerable<Room> GetAllTheoryRoom();
        IEnumerable<Room> GetAllLabRoom();
    }
}

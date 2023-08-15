namespace Timetable.Application.Persistence.Repository
{
    public interface ISemesterRepository : IGenericRepository<Semester>
    {
        public void Clear();
    }
}

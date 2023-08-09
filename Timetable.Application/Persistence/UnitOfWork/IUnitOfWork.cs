using Timetable.Application.Persistence.Repository;

namespace Timetable.Application.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITeacherRepository Teachers { get; }
        int Complete();
    }
}
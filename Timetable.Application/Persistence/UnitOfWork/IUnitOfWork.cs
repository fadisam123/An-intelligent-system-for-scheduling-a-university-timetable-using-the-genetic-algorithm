using Timetable.Application.Persistence.Repository;

namespace Timetable.Application.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITeacherRepository TeacherRepository { get; }
        ICourseRepository CourseRepository { get; }
        IRoomRepository RoomRepository { get; }
        IYearRepository YearRepository { get; }
        ISemesterRepository SemesterRepository { get; }
        int SaveChanges();
    }
}
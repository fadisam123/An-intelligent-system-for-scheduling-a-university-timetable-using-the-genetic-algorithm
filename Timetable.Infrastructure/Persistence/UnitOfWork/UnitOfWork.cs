using Timetable.Application.Persistence.Repository;
using Timetable.Application.Persistence.UnitOfWork;
using Timetable.Infrastructure.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Teachers = new TeacherRepository(_context);
        }
        public ITeacherRepository Teachers { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
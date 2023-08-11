using Timetable.Application.Persistence.Repository;
using Timetable.Application.Persistence.UnitOfWork;

namespace Timetable.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context, ITeacherRepository teacherRepository,
            ICourseRepository courseRepository, IRoomRepository roomRepository,
            IYearRepository yearRepository, ISemesterRepository semesterRepository)
        {
            _context = context;
            TeacherRepository = teacherRepository;
            CourseRepository = courseRepository;
            RoomRepository = roomRepository;
            YearRepository = yearRepository;
            SemesterRepository = semesterRepository;
        }
        public ITeacherRepository TeacherRepository { get; private set; }
        public ICourseRepository CourseRepository { get; private set; }
        public IRoomRepository RoomRepository { get; private set; }
        public IYearRepository YearRepository { get; }
        public ISemesterRepository SemesterRepository { get; }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
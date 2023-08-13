using Timetable.Application.Persistence.Repository;
using Timetable.Application.Persistence.UnitOfWork;

namespace Timetable.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context, ITeacherRepository teacherRepository,
            ICourseRepository courseRepository, IRoomRepository roomRepository,
            IYearRepository yearRepository, ISemesterRepository semesterRepository,
            IDayRepository dayRepository, ITimeRepository timeRepository,
            ILectureRepository lectureRepository)
        {
            _context = context;
            TeacherRepository = teacherRepository;
            CourseRepository = courseRepository;
            RoomRepository = roomRepository;
            YearRepository = yearRepository;
            SemesterRepository = semesterRepository;
            DayRepository = dayRepository;
            TimeRepository = timeRepository;
            LectureRepository = lectureRepository;
        }
        public ITeacherRepository TeacherRepository { get; private set; }
        public ICourseRepository CourseRepository { get; private set; }
        public IRoomRepository RoomRepository { get; private set; }
        public IYearRepository YearRepository { get; private set; }
        public ISemesterRepository SemesterRepository { get; private set; }
        public IDayRepository DayRepository { get; private set; }
        public ITimeRepository TimeRepository { get; private set; }
        public ILectureRepository LectureRepository { get; private set; }

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
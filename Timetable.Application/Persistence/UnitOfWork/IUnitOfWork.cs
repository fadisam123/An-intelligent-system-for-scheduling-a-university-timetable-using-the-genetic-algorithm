using Timetable.Application.Persistence.Repository;
using Timetable.Application.Services.DataIO.DayTime;

namespace Timetable.Application.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public ITeacherRepository TeacherRepository { get; }
        public ICourseRepository CourseRepository { get; }
        public IRoomRepository RoomRepository { get; }
        public IYearRepository YearRepository { get; }
        public ISemesterRepository SemesterRepository { get; }
        public IDayRepository DayRepository { get; }
        public ITimeRepository TimeRepository { get; }
        public ILectureRepository LectureRepository { get; }
        public ISurveyRepository SurveyRepository { get; }
        public ITeacherPreferenceDayTimesRepository TeacherPreferenceDayTimesRepository { get; }
        public int SaveChanges();
    }
}
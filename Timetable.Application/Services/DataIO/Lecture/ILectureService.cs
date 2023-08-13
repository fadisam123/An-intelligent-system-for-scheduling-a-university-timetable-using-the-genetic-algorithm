namespace Timetable.Application.Services.DataIO.DayTime
{
    public interface ILectureService
    {
        public IEnumerable<Lecture> GenerateTheorySchedule(Semester semester);
        public IEnumerable<Lecture> GenerateLabSchedule();
        public IEnumerable<Lecture> GetTheorySchedule(Semester semester, Year year);
        public IEnumerable<Lecture> GetLabSchedule();
    }
}

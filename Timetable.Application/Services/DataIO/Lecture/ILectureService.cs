namespace Timetable.Application.Services.DataIO.DayTime
{
    public interface ILectureService
    {
        public void AddLecture(Lecture lecture);
        public void DeleteLectureById(Guid lectureId);
        public IEnumerable<Lecture> GenerateTheorySchedule(Semester semester);
        public IEnumerable<Lecture> GenerateLabSchedule();
        public IEnumerable<Lecture> GetTheorySchedule(Semester semester, Year year);
        public IEnumerable<Lecture> GetLabSchedule();
    }
}

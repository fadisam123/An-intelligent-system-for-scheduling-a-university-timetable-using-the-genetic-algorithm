namespace Timetable.Application.Services.DataIO.SemesterYear
{
    public interface ISemesterYearService
    {
        public IEnumerable<Year> getAllYears();
        public IEnumerable<Semester> getAllSemesters();
    }
}

namespace Timetable.Application.Services.DataIO.SemesterYear
{
    public interface ISemesterYearService
    {
        public IEnumerable<Year> getAllYears();
        public IEnumerable<Semester> getAllSemesters();
        public void DeleteAllSemesters();
        public void DeleteAllYears();
        public void AddSemesters(Semester[] semesters);
        public void AddYears(Year[] years);
    }
}

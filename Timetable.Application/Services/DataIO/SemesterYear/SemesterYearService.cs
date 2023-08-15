using Timetable.Application.Persistence.UnitOfWork;
using Timetable.Domain.Entities;

namespace Timetable.Application.Services.DataIO.SemesterYear
{
    public class SemesterYearService : ISemesterYearService
    {
        private IUnitOfWork Uow { get; }
        public SemesterYearService(IUnitOfWork uow)
        {
            Uow = uow;
        }
        public IEnumerable<Semester> getAllSemesters()
        {
            return Uow.SemesterRepository.GetAll();
        }

        public IEnumerable<Year> getAllYears()
        {
            return Uow.YearRepository.GetAll();
        }

        public void DeleteAllSemesters()
        {
            Uow.SemesterRepository.Clear();
        }

        public void DeleteAllYears()
        {
            Uow.YearRepository.Clear();
        }

        public void AddSemesters(Semester[] semesters)
        {
            Uow.SemesterRepository.AddRange(semesters);
            Uow.SaveChanges();
        }

        public void AddYears(Year[] years)
        {
            Uow.YearRepository.AddRange(years);
            Uow.SaveChanges();
        }
    }
}

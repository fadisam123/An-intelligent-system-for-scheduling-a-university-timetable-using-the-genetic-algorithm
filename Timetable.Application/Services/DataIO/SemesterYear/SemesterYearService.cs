using Timetable.Application.Persistence.UnitOfWork;

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
    }
}

using Timetable.Application.Persistence.UnitOfWork;

namespace Timetable.Application.Services.DataIO.DayTime
{
    public class DayTimeService : IDayTimeService
    {
        private IUnitOfWork Uow { get; }
        public DayTimeService(IUnitOfWork uow)
        {
            Uow = uow;
        }
        public void AddDay(Day day)
        {
            Uow.DayRepository.Add(day);
        }

        public void DeleteDay(Day day)
        {
            Uow.DayRepository.Remove(day);
        }

        public IEnumerable<Day> GetAllDays()
        {
            return Uow.DayRepository.GetAll();
        }

        public IEnumerable<Time> GetAllTimes()
        {
            return Uow.TimeRepository.GetAll();
        }

        public Day GetDayById(int dayId)
        {
            return Uow.DayRepository.Find(d => d.DayNo == dayId).First();
        }

        public Time GetTimeById(Guid timeId)
        {
            return Uow.TimeRepository.GetById(timeId);
        }
    }
}

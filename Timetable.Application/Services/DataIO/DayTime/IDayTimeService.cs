namespace Timetable.Application.Services.DataIO.DayTime
{
    public interface IDayTimeService
    {
        public void AddDay(Day day);
        public void DeleteDay(Day day);
        public IEnumerable<Day> GetAllDays();
        public IEnumerable<Time> GetAllTimes();
        public Day? GetDayById(int dayId);
        public Time GetTimeById(Guid timeId);
        public void RemoveDays();
        public void RemoveTimes();
        public void AddTimes(Time[] times);
        public void AddDays(Day[] days);

    }
}

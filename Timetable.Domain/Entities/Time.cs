namespace Timetable.Domain.Entities
{
    public class Time : Base
    {
        #region Properties
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ICollection<TeacherPreferenceDayTime> TeacherPreferenceDayTimes { get; } = new List<TeacherPreferenceDayTime>();
        public virtual ICollection<Lecture> Lectures { get; } = new List<Lecture>();
        #endregion
    }
}

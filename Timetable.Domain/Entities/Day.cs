namespace Timetable.Domain.Entities
{
    public class Day : IEntity
    {
        #region Properties
        [Key]
        public int DayNo { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        #endregion

        #region Navigation Properties
        public virtual ICollection<TeacherPreferenceDayTime> TeacherPreferenceDayTimes { get; } = new List<TeacherPreferenceDayTime>();
        public virtual ICollection<Lecture> Lectures { get; } = new List<Lecture>();
        #endregion
    }
}

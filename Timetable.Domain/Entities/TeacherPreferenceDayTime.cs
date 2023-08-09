namespace Timetable.Domain.Entities
{
    public class TeacherPreferenceDayTime : Base
    {
        #region Navigation Properties
        public virtual Day day { get; set; } = null!;
        public virtual Time time { get; set; } = null!;
        public virtual User user { get; set; } = null!;
        #endregion
    }
}
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Domain.Entities
{
    public class Room : Base
    {
        #region Properties
        public String Name { get; set; } = null!;
        public RoomTypeEnum type { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ICollection<Lecture> Lectures { get; } = new List<Lecture>();
        public virtual ICollection<Course>? Courses { get; set; }
        #endregion

    }
}

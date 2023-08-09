using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Domain.Entities
{
    public class Lecture : Base
    {
        #region Properties
        public LectureTypeEnum Type { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Time Time { get; set; } = null!;
        public virtual Day day { get; set; } = null!;
        public virtual Group? group { get; set; }
        public virtual Course course { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        #endregion
    }
}

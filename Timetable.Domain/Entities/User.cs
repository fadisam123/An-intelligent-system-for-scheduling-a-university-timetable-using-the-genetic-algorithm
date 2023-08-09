using Microsoft.AspNetCore.Identity;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Domain.Entities
{
    public class User : IdentityUser<Guid>, IEntity
    {
        #region Properties
        public string Name { get; set; } = null!;
        public UserTypeEnum Type { get; set; } = UserTypeEnum.Professor;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        #endregion

        #region Navigation Properties
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<TeacherPreferenceDayTime> Preferences { get; set; } = new List<TeacherPreferenceDayTime>();
        #endregion
    }
}

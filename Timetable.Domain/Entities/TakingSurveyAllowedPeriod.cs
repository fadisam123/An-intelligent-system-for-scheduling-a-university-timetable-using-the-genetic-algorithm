using System.ComponentModel.DataAnnotations.Schema;

namespace Timetable.Domain.Entities
{
    public class TakingSurveyAllowedPeriod : Base
    {
        #region Properties
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Guid RoleID { get; set; }
        public virtual Role role { get; set; } = null!;
        #endregion
    }
}

using Microsoft.AspNetCore.Identity;

namespace Timetable.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        #region Navigation Properties
        public virtual TakingSurveyAllowedPeriod? SurveyPeriod { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        #endregion
    }
}

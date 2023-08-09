namespace Timetable.Domain.Entities
{
    public class Semester : IEntity
    {
        #region Properties
        [Key]
        public int SemesterNo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        #endregion

        #region Navigation Properties
        public virtual ICollection<Course> Courses { get; } = new List<Course>();
        #endregion
    }
}

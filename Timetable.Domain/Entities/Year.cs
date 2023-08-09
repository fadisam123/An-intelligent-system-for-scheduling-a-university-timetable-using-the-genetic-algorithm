namespace Timetable.Domain.Entities
{
    public class Year : IEntity
    {
        #region Properties
        [Key]
        public int YearNo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        #endregion

        #region Navigation Properties
        public virtual ICollection<Course> Courses { get; } = new List<Course>();
        #endregion
    }
}

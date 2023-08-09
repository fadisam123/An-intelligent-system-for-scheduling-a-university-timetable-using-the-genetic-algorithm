namespace Timetable.Domain.Entities
{
    public class Group : IEntity
    {
        #region Properties
        [Key]
        public int GroupNo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        #endregion

        #region Navigation Properties
        public virtual ICollection<Lecture> Lectures { get; } = new List<Lecture>();
        #endregion
    }
}

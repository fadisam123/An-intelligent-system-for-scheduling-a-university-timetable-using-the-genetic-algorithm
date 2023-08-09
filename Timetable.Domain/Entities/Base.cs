namespace Timetable.Domain.Entities
{
    public class Base : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

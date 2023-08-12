namespace Timetable.RazorWeb.ViewModels.OutputModels
{
    public class PracticalDeptOutputModel
    {
        public User teacher { get; set; } = null!;
        public Course course { get; set; } = null!;
        public Room room { get; set; } = null!;
    }
}

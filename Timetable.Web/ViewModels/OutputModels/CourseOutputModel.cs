namespace Timetable.RazorWeb.ViewModels.OutputModels
{
    public class CourseView
    {
        public string Id = null!;
        public string Name { get; set; } = null!;
        public int LuctureNumPerWeek { get; set; } = 1;
        public bool HasPracticalSection { get; set; } = false;
        public bool IsElective { get; set; } = false;
        public int Year { get; set; } = 1;
        public int Semester { get; set; } = 1;
        public CourseTypeEnum Type { get; set; } = CourseTypeEnum.TheoryCourse;
    }
    public class CourseOutputModel
    {
        public List<Year> Years { get; set; } = new List<Year>();
        public List<Semester> Semesters { get; set; } = new List<Semester>();
        public List<CourseView> Courses { get; set; } = new List<CourseView>();
    }
}

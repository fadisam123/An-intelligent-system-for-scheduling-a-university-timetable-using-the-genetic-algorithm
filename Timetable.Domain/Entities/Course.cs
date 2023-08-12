using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Domain.Entities
{
    public class Course : Base
    {
        #region Properties
        public String Name { get; set; } = null!;
        public int LuctureNumPerWeek { get; set; }
        public CourseTypeEnum Type { get; set; }
        public bool IsElective { get; set; } = false;
        #endregion

        #region Navigation Properties
        public virtual Year year { get; set; } = null!;
        public virtual Semester semester { get; set; } = null!;
        public virtual Room? TeacherpreferredRoom { get; set; }
        public virtual User? user { get; set; } = null;
        public virtual ICollection<Lecture> Lectures { get; } = new List<Lecture>();
        #endregion

    }
}

namespace Model
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Lecture_num_per_week { get; set; }

        public int Semester { get; set; }
        public int Year { get; set; }
        public Teacher Teacher { get; set; }
        public ClassRoom preferredRoom { get; set; }

        public Course(string name, int year, int semester, int lecture_num_per_week, Teacher teacher, ClassRoom preferredRoom)
        {
            Id = Guid.NewGuid();
            Name = name;
            Lecture_num_per_week = lecture_num_per_week;
            Year = year;
            Semester = semester;
            Teacher = teacher;
            this.preferredRoom = preferredRoom;
        }
    }
}

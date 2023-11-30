namespace Model
{
    public class Lecture
    {
        public byte Day { get; set; }
        public TimeOnly Time { get; set; }

        public ClassRoom ClassRoom { get; set; }
        public Course Course { get; set; }

        public Lecture(byte day, TimeOnly time, ClassRoom classRoom, Course course)
        {
            Day = day;
            Time = time;
            ClassRoom = classRoom;
            Course = course;
        }

        public override string ToString()
        {
            return ("Year: " + Course.Year + "\tDay: " + Day + "\tTime: " + Time + "\tRoom: " + ClassRoom.Name + "\tCourse: " + Course.Name + "\tTeacher: " + Course.Teacher.Name);
        }
    }
}

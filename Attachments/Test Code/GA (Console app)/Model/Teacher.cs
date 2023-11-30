namespace Model
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<PreferredDayTime> ListOfPreferredDayTime { get; set; }

        public Teacher(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        public Teacher(string name, List<PreferredDayTime> listOfPreferredDayTime)
        {
            Id = Guid.NewGuid();
            Name = name;
            ListOfPreferredDayTime = listOfPreferredDayTime;
        }
    }
}

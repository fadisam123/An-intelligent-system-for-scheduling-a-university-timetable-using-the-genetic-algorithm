namespace Model
{
    public class ClassRoom
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // (False) for Theoretical room, and (True) for Labs 
        public bool Room_Type { get; set; }
        public int Max_Capacity { get; set; }

        public ClassRoom(string name, int max_Capacity, bool room_Type = false)
        {
            Id = Guid.NewGuid();
            Name = name;
            Room_Type = room_Type;
            Max_Capacity = max_Capacity;    
        }
    }
}

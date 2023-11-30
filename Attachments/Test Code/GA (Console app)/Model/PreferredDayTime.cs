namespace Model
{
    public class PreferredDayTime
    {
        public byte preferredDay { get; set; }
        public TimeOnly preferredTime { get; set; }
        public PreferredDayTime(byte preferredDay, TimeOnly preferredTime)
        {
            this.preferredDay = preferredDay;
            this.preferredTime = preferredTime;
        }
        public override string ToString()
        {
            return "Day: " + preferredDay + ", Time: " + preferredTime;
        }
    }
}

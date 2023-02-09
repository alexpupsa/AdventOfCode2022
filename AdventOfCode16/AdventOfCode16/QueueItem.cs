namespace AdventOfCode16
{
    public class QueueItem
    {
        public string ValveName { get; set; }
        public int Minutes { get; set; }
        public List<string> OpenValves { get; set; }
        public int CurrentRate { get; set; }
    }
}

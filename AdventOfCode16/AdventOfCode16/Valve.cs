namespace AdventOfCode16
{
    public class Valve
    {
        public string Name { get; set; }
        public int FlowRate { get; set; }
        public string[]? ConnectedValves { get; set; }

        public Valve(string name, int flowRate, string[]? connectedValves)
        {
            Name = name;
            FlowRate = flowRate;
            ConnectedValves = connectedValves;
        }
    }
}

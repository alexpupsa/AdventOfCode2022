namespace AdventOfCode11
{
    public class Monkey
    {
        public int Number { get; set; }
        public List<long> Items { get; set; } = new List<long>();
        public Operation Operation { get; set; }
        public long OperationValue { get; set; }
        public long DivisibleBy { get; set; }
        public int MonkeyTrue { get; set; }
        public int MonkeyFalse { get; set; }
        public long InspectedItems { get; set; }
    }
}

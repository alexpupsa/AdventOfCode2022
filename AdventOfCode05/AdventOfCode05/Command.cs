namespace AdventOfCode05
{
    public record Command
    {
        public int Quantity { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}

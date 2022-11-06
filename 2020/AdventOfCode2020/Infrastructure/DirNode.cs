namespace AdventOfCode.Infrastructure
{
    public class DirNode
    {
        public DirNode Prev { get; set; }
        public DirNode Next { get; set; }
        public string Direction { get; set; }
    }
}

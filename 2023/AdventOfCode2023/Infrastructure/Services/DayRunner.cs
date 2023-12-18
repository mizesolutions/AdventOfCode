using AdventOfCode2023.Days;

namespace AdventOfCode2023.Infrastructure.Services
{
    public class DayRunner
    {
        public string Day { get; set; }
        public bool IsInput { get; set; }
        public Dictionary<string, BaseDay> DayRun { get; set; }

        public DayRunner(string d, bool b = true)
        {
            Day = d;
            IsInput = b;
            CreateDayDictionary();
        }

        private void CreateDayDictionary()
        {
            DayRun = new Dictionary<string, BaseDay>()
            {
                //{ "day01", new Day01("day01", IsInput)},
                //{ "day02", new Day02("day02", IsInput)},
                //{ "day03", new Day03("day03", IsInput)},
                //{ "day04", new Day04("day04", IsInput)},
                //{ "day05", new Day05("day05", IsInput)},
                //{ "day06", new Day06("day06", IsInput)},
                //{ "day07", new Day07("day07", IsInput)},
                //{ "day08", new Day08("day08", IsInput)},
                //{ "day09", new Day09("day09", IsInput)},
                //{ "day10", new Day10("day10", IsInput)},
                //{ "day11", new Day11("day11", IsInput)},
                //{ "day12", new Day12("day12", IsInput)},
                //{ "day13", new Day13("day13", IsInput)},
                //{ "day14", new Day14("day14", IsInput)},
                { "day15", new Day15("day15", IsInput)},
                //{ "day16", new Day16("day16", IsInput)},
                //{ "day17", new Day17("day17", IsInput)},
                //{ "day18", new Day18("day18", IsInput)},
                //{ "day19", new Day19("day19", IsInput)},
                //{ "day20", new Day20("day20", IsInput)},
                //{ "day21", new Day21("day21", IsInput)},
                //{ "day22", new Day22("day22", IsInput)},
                //{ "day23", new Day23("day23", IsInput)},
                //{ "day24", new Day24("day24", IsInput)},
                //{ "day25", new Day25("day25", IsInput)}
            };
        }

        public dynamic RunDay()
        {
            return DayRun[Day];
        }
    }
}

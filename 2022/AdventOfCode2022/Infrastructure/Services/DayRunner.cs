using AdventOfCode2022.Days;
using System.Collections.Generic;

namespace AdventOfCode2022.Infrastructure.Services
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
                { "day01", new Day01("day01", IsInput)},
                { "day02", new Day02("day02", IsInput)},
                { "day03", new Day03("day03", IsInput)},
                { "day04", new Day04("day04", IsInput)},
                { "day05", new Day05("day05", IsInput)},
                { "day06", new Day06("day06", IsInput)},
                { "day07", new Day07("day07", IsInput)},
                { "day08", new Day08("day08", IsInput)},
                { "day09", new Day09("day09", IsInput)},
                { "day10", new Day10("day10", IsInput)},
            };
        }

        public dynamic RunDay()
        {
            return DayRun[Day];
        }
    }
}

using AdventOfCode2022.Days.Day00;
using AdventOfCode2022.Days.Day01;
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
            };
        }

        public dynamic RunDay()
        {
            return DayRun[Day];
        }
    }
}

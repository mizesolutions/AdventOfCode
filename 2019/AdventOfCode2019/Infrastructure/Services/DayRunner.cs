﻿using AdventOfCode2019.Days;
using System.Collections.Generic;

namespace AdventOfCode2019.Infrastructure.Services
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
            };
        }

        public dynamic RunDay()
        {
            return DayRun[Day];
        }
    }
}

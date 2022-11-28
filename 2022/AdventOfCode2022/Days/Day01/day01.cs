using AdventOfCode2022.Days.Day00;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day01
{
    public class Day01 : BaseDay
    {
        private List<int> IntInput { get; set; }

        public Day01(string day, bool hasInput) : base(day, hasInput)
        {
            IntInput = FileInput.Select(int.Parse).ToList();
            Result1 = 0;
        }

        public void PuzzleOne()
        {
            PrintCurrentMethod();
            PrintResults(Result1);
        }

        public void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }
    }
}

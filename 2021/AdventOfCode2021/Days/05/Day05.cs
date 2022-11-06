using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day05 : BaseDay
    {
        public Day05(string day, bool hasInput) : base(day, hasInput)
        {
            PrintCurrentClass();
            PuzzleOne();
            PuzzleTwo();
        }

        private void PuzzleOne()
        {
            PrintCurrentMethod();
            PrintResults(Result1);
        }

        private void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }
    }
}

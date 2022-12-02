using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
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
            for (var i = 0; i <= IntInput.Count - 2; i++)
            {
                int j = i + 1;
                if (IntInput.ElementAt(j) > IntInput.ElementAt(i))
                {
                    Result1++;
                }
            }
            PrintResults(Result1);
        }

        public void PuzzleTwo()
        {
            PrintCurrentMethod();
            Result2 = 0;
            for (int i = 0, j = i + 1; j <= IntInput.Count - 3; i++, j++)
            {
                if ((IntInput.ElementAt(j) + IntInput.ElementAt(j + 1) + IntInput.ElementAt(j + 2)) > (IntInput.ElementAt(i) + IntInput.ElementAt(i + 1) + IntInput.ElementAt(i + 2)))
                {
                    Result2++;
                }
            }
            PrintResults(Result2);
        }
    }
}

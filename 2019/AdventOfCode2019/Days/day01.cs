using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    public class Day01 : BaseDay
    {

        public Day01(string day, bool hasInput) : base(day, hasInput)
        {
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            foreach(var m in FileInput)
            {
                Result1 += Convert.ToInt32(Math.Floor((decimal)(int.Parse(m) / 3)) - 2);
                FuelCalculator(Convert.ToInt32(Math.Floor((decimal)(int.Parse(m) / 3)) - 2));
            }
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }

        private int FuelCalculator(int mass)
        {
            if(mass < 0) { return 0; }
            Result2 += mass;
            return FuelCalculator(Convert.ToInt32(Math.Floor((decimal)(mass / 3)) - 2));
        }
    }
}

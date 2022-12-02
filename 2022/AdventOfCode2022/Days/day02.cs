using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day02 : BaseDay
    {

        public Day02(string day, bool hasInput) : base(day, hasInput)
        {
            Result1 = 0;
            Result2 = 0;
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            foreach (var r in FileInput)
            {
                Result1 += RPSOutcome1(r);
                Result2 += RPSOutcome2(r);
            }
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }

        private int RPSOutcome1(string round) =>
            round switch
            {
                "A X" => 4,
                "A Y" => 8,
                "A Z" => 3,
                "B X" => 1,
                "B Y" => 5,
                "B Z" => 9,
                "C X" => 7,
                "C Y" => 2,
                "C Z" => 6,
                _ => 0,
            };

        private int RPSOutcome2(string round) =>
            round switch
            {
                "A X" => 3,
                "A Y" => 4,
                "A Z" => 8,
                "B X" => 1,
                "B Y" => 5,
                "B Z" => 9,
                "C X" => 2,
                "C Y" => 6,
                "C Z" => 7,
                _ => 0,
            };
    }
}

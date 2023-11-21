using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public class Day10 : BaseDay
    {
        public int Cycle { get; set; }
        public int XRegister { get; set; }

        public Day10 (string day, bool hasInput) : base(day, hasInput)
        {
            Cycle = 0;
            XRegister = 1;
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            Execution();
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }

        private void Execution()
        {
            foreach(var line in FileInput)
            {
                Cycle++;
                if ((Cycle - 20) % 40 == 0)
                {
                    Result1 += Cycle * XRegister;
                }
                if (line[0] == 'a')
                {
                    Cycle++;
                    if ((Cycle - 20) % 40 == 0)
                    {
                        Result1 += Cycle * XRegister;
                    }
                    XRegister += int.Parse(line[5..]);
                }
            }

        }
    }
}
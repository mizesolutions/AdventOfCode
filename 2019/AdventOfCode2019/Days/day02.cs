using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    public class Day02 : BaseDay
    {
        private int[] CodeArray { get; set; }

        public Day02 (string day, bool hasInput) : base(day, hasInput)
        {
            Input.MixedDataRender();
            CodeArray = Input.IntList.ToArray();
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            for (var i = 0; i < CodeArray.Length; i += 4)
            {
                CodeCompute(i, CodeArray[i]);
            }
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }

        private int CodeCompute(int i, int value) =>
            value switch
            {
                99 => Result1 = CodeArray[0],
                1 => CodeArray[CodeArray[i + 3]] = CodeArray[CodeArray[i + 1]] + CodeArray[CodeArray[i + 2]],
                2 => CodeArray[CodeArray[i + 3]] = CodeArray[CodeArray[i + 1]] * CodeArray[CodeArray[i + 2]],
                _ => Result1 = CodeArray[0]
            };
    }
}

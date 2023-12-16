using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection.Emit;

namespace AdventOfCode2023.Days
{
    public class Day15 : BaseDay
    {
        public List<string> SeqList { get; set; }
        public OrderedDictionary[] Lenses { get; set; }

        public Day15(string day, bool hasInput) : base(day, hasInput)
        {
            SeqList = new List<string>();
            Lenses = new OrderedDictionary[256];
            for (var i = 0; i < Lenses.Length; i++)
            {
                Lenses[i] = new OrderedDictionary();
            }
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();

            SeqList = FileInput.FirstOrDefault().Split(',').ToList();
            foreach (var seq in SeqList)
            {
                Result1 += GetValue(seq);
            }

            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();

            foreach (var seq in SeqList)
            {
                var label = seq.Substring(0, 2);
                var boxNum = GetValue(label);
                var lens = 0;

                if (seq.Contains('='))
                {
                    lens = int.Parse(seq.Substring(seq.IndexOf('=') + 1));
                }
                Operation(boxNum, label, lens);
            }

            PrintResults(Result2);
        }


        private long GetValue(string seq)
        {
            long curr = 0;
            foreach (char c in seq)
            {
                curr += (int)c;
                curr *= 17;
                curr %= 256;
            }
            return curr;
        }

        private void Operation(long index, string lable, int lens)
        {
            if (lens == 0 && Lenses[index] != null && Lenses[index].Contains(lable))
            {
                Lenses[index].Remove(lable);
            }
            else
            {
                if (Lenses[index] != null && Lenses[index].Contains(lable))
                {
                    Lenses[index][lable] = lens;
                }
                else
                {
                    Lenses[index].Add(lable, lens);
                }
            }
        }

    }
}
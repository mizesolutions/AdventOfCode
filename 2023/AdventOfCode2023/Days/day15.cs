using System.Collections.Specialized;

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

            SeqList = [.. FileInput.FirstOrDefault().Split(',')];
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
                var label = seq[..seq.IndexOfAny(['-', '=', '1', '2', '3', '4', '5', '6', '7', '8', '9'])];
                var boxNum = GetValue(label);
                var lens = 0;

                if (seq.Contains('='))
                {
                    lens = int.Parse(seq[(seq.IndexOf('=') + 1)..]);
                }
                Operation(boxNum, label, lens);
            }
            Result2 = CalculateFocusPower();

            PrintResults(Result2);
        }


        private static long GetValue(string seq)
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
            if (lens == 0)
            {
                if (Lenses[index] != null && Lenses[index].Contains(lable))
                {
                    Lenses[index].Remove(lable);
                }

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

        private long CalculateFocusPower()
        {
            long result = 0;
            for (int i = 0; i < Lenses.Length; i++)
            {
                int[] lensValues = new int[Lenses[i].Count];
                Lenses[i].Values.CopyTo(lensValues, 0);
                int j = 1;
                int k = 0;
                while (k < lensValues.Length)
                {
                    result += (1 + i) * j * lensValues[k];
                    k++;
                    j++;
                }
            }
            return result;
        }

    }
}
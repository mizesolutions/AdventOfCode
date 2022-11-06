using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    class Day03 : BaseDay
    {
        private int[] MCcount { get; set; }
        private int[] LCcount { get; set; }

        public Day03(string day, bool hasInput) : base(day, hasInput)
        {
        }

        public void PuzzleOne()
        {
            PrintCurrentMethod();
            Result1 = 0;
            ComputeCommons(FileInput);

            var gamma = Convert.ToInt32(string.Join("", MCcount), 2);
            var epsilon = Convert.ToInt32(string.Join("", LCcount), 2);

            Result1 = gamma * epsilon;
            PrintResults(Result1);
        }

        public void PuzzleTwo()
        {
            PrintCurrentMethod();
            Result2 = 0;
            List<string> o2 = FilterList(FileInput, MCcount, 0, true);
            ComputeCommons(FileInput);
            List<string> co2 = FilterList(FileInput, LCcount, 0, false);

            var o2Int = Convert.ToInt32(o2.FirstOrDefault().ToString(), 2);
            var co2Int = Convert.ToInt32(co2.FirstOrDefault().ToString(), 2);

            Result2 = o2Int * co2Int;
            PrintResults(Result2);
        }

        private void ComputeCommons(List<string> list)
        {
            MCcount = new int[FileInput[0].Length];
            LCcount = new int[FileInput[0].Length];

            foreach (var s in list)
            {
                for (var i = 0; i < s.Length; i++)
                {
                    if (s[i].Equals('1'))
                        MCcount[i]++;
                }
            }

            for (var i = 0; i < MCcount.Length; i++)
            {
                if (list.Count - MCcount[i] <= MCcount[i])
                {
                    MCcount[i] = 1;
                    LCcount[i] = 0;
                }
                else
                {
                    MCcount[i] = 0;
                    LCcount[i] = 1;
                }
            }
        }

        private List<string> FilterList(List<string> list, int[] filterByArray, int index, bool mostCommon)
        {
            if (list.Count == 1 || index == FileInput[0].Length)
                return list;

            var filteredList = list.Where(x => x.Substring(index).StartsWith(filterByArray[index].ToString())).ToList();
            ComputeCommons(filteredList);
            filterByArray = mostCommon ? MCcount : LCcount;
            index++;
            return FilterList(filteredList, filterByArray, index, mostCommon);
        }
    }
}

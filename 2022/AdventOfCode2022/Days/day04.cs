using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day04 : BaseDay
    {
        List<int[]> Ids { get; set; }

        public Day04 (string day, bool hasInput) : base(day, hasInput)
        {
            Ids = IdsToInts();
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            foreach(var idArray in Ids)
            {
                var range1 = Enumerable.Range(idArray[0], (idArray[1] - idArray[0]) + 1).ToList();
                var range2 = Enumerable.Range(idArray[2], (idArray[3] - idArray[2]) + 1).ToList();
                Result1 += IdSubSet(range1, range2);
                Result2 += IdIntersection(range1, range2);
            }
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }

        private List<int[]> IdsToInts()
        {
            List <int[]> idTemp = new();
            String[] delimiters = { "-", "," };
            foreach(var i in FileInput)
            {
                var arr = i.Split(delimiters, StringSplitOptions.None);
                idTemp.Add(Array.ConvertAll(arr, int.Parse));
            }
            return idTemp;
        }

        private int IdSubSet(List<int> range1, List<int> range2)
        {
            return range1.All(i => range2.Contains(i)) || range2.All(i => range1.Contains(i)) ? 1 : 0;
        }

        private int IdIntersection(List<int> range1, List<int> range2)
        {
            return range1.Intersect(range2).FirstOrDefault() > 0 ? 1 : 0;
        }
    }
}
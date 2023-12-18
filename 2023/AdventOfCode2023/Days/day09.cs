namespace AdventOfCode2023.Days
{
    public class Day09 : BaseDay
    {

        public Day09(string day, bool hasInput) : base(day, hasInput)
        {
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();

            foreach (var i in FileInput)
            {
                long sum = 0;
                var iList = new List<int>();
                var sList = i.Split(' ');
                foreach (var s in sList)
                {
                    bool v = int.TryParse(s, out var num);
                    if (v)
                    {
                        iList.Add(num);
                    }
                }
                Result1 += ReportSum(iList, sum);
            }


            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }


        private static long ReportSum(List<int> list, long sum)
        {
            if (list.All(x => x.Equals(0)))
            {
                return sum = 0;
            }
            var tempList = new List<int>();
            for (var i = 0; i < list.Count - 1; i++)
            {
                var tempNum = 0;
                tempNum = list[i] - list[i + 1];
                if (tempNum < 0)
                {
                    tempNum *= -1;
                }
                tempList.Add(tempNum);
            }
            sum = ReportSum(tempList, sum);
            return sum + list.Last();
        }

    }
}
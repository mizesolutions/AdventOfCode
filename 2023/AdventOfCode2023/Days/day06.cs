
namespace AdventOfCode2023.Days
{
    public class Day06 : BaseDay
    {
        public List<long> WinProbability { get; set; }
        public List<List<int>> Data { get; set; }
        public List<long> PartTwoData { get; set; }


        public Day06(string day, bool hasInput) : base(day, hasInput)
        {
            WinProbability = new List<long>();
            Data = new List<List<int>>();
            PartTwoData = new List<long>();
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            foreach (var i in FileInput)
            {
                var temp = i.Split(' ').ToList();
                temp.RemoveAt(0);
                temp = temp.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
                List<int> intTemp = new List<int>();
                foreach (var c in temp)
                {
                    int.TryParse(c, out var value);
                    intTemp.Add(value);
                }
                Data.Add(intTemp);
            }

            for (var i = 0; i < Data[0].Count; i++)
            {
                int option = 0;
                int time = Data[0][i];
                int distance = Data[1][i];
                int result = 0;
                while (option <= time)
                {
                    if (((time - option) * option) > distance)
                    {
                        result++;
                    }
                    option++;
                }
                WinProbability.Add(result);
            }
            Result1 = WinProbability.Aggregate((a, x) => a * x);

            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            WinProbability = new List<long>();

            foreach (var i in FileInput)
            {
                var iTemp = i.Substring(9);
                var temp = RemoveWhitespace(iTemp);
                long.TryParse(temp, out var value);
                PartTwoData.Add(value);
            }

            long option = 0;
            long time = PartTwoData[0];
            long distance = PartTwoData[1];
            long result = 0;
            while (option <= time)
            {
                if (((time - option) * option) > distance)
                {
                    result++;
                }
                option++;
            }

            Result2 = result;
            PrintResults(Result2);
        }

        private string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
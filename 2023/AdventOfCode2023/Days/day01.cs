namespace AdventOfCode2023.Days
{
    public class Day01 : BaseDay
    {
        public string First { get; set; }
        public string Second { get; set; }
        public List<string> CalibrationValues { get; set; }

        public Day01(string day, bool hasInput) : base(day, hasInput)
        {
            First = "";
            Second = "";
            CalibrationValues = new List<string>();
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            foreach (var s in FileInput)
            {
                First = "";
                Second = "";
                foreach (var c in s)
                {
                    if (Char.IsDigit(c))
                    {
                        var num = c.ToString();
                        if (string.IsNullOrEmpty(First))
                        {
                            First = num;
                        }
                        else
                        {
                            Second = num;
                        }
                    }
                }

                if (string.IsNullOrEmpty(Second))
                {
                    CalibrationValues.Add(string.Concat(new[] { First, First }));
                }
                else
                {
                    CalibrationValues.Add(string.Concat(new[] { First, Second }));
                }
            }
            foreach (var num in CalibrationValues)
            {
                if (Int64.TryParse(num, out var result))
                    Result1 += result;
            }
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }
    }
}
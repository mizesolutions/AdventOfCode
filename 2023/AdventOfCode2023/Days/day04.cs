namespace AdventOfCode2023.Days
{
    public class Day04 : BaseDay
    {

        public Day04(string day, bool hasInput) : base(day, hasInput)
        {
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            List<List<string>> splitStrings = new();
            foreach (var s in FileInput)
            {
                var t = s.Split(':', '|').ToList<string>();
                splitStrings.Add(t);
            }

            for (int i = 0; i < splitStrings.Count; i++)
            {
                splitStrings[i][1].TrimStart();
                splitStrings[i][2].TrimStart();
            }

            foreach (var t in splitStrings)
            {
                if (t[0].ToLower().Equals('c'))
                {
                    continue;
                }
                else
                {
                    var numbers1 = t[1].Split();
                    var numbers2 = t[2].Split();

                    var results = numbers1.Intersect(numbers2).ToList();
                    results.RemoveAll(s => s == "");
                    //foreach (var result in results)
                    //{
                    //    Console.WriteLine(result);
                    //}
                    if (results.Count > 0)
                    {
                        var points = 0;
                        for (int i = 1; i <= results.Count; i++)
                        {
                            if (i == 1)
                            {
                                points = 1;
                            }
                            else
                            {
                                points = points * 2;
                            }
                        }
                        Result1 += points;
                    }
                }
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
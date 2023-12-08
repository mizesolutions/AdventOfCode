
namespace AdventOfCode2023.Days
{
    public class Day08 : BaseDay
    {
        public Char[] Directions { get; set; }
        public List<(string Position, string Left, string Right)> Map { get; set; }
        public List<(string Position, string Left, string Right)> MultiNodeMap { get; set; }

        public Day08(string day, bool hasInput) : base(day, hasInput)
        {
            Directions = [];
            Map = [];
            MultiNodeMap = [];
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();

            foreach (var l in FileInput)
            {
                if (l.Contains('='))
                {
                    var position = l.Substring(0, 3);
                    var left = l.Substring(7, 3);
                    var right = l.Substring(12, 3);

                    Map.Add((position, left, right));
                }
                else if (!l.Contains('=') && l.StartsWith('L') || l.StartsWith('R'))
                {
                    Directions = l.ToCharArray();
                }
                else
                {
                    continue;
                }
            }


            int leftCount = Directions.Count(x => x == 'L');
            int rightCount = Directions.Count(x => x == 'R');

            var next = "aaa";
            var steps = 0;
            for (var i = 0; i < Directions.Length; i++)
            {
                int index = Map.FindIndex(a => a.Position.ToLower().Contains(next));
                if (Directions[i] == 'L')
                {
                    next = Map[index].Left.ToLower();
                    steps++;
                }
                else
                {
                    next = Map[index].Right.ToLower();
                    steps++;
                }

                if (next.ToLower().Equals("zzz"))
                {
                    break;
                }

                if (i == Directions.Length - 1)
                {
                    i = -1;
                }
            }

            Result1 = steps;
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();

            foreach (var i in Map)
            {
                if (i.Position.ToLower().EndsWith("a"))
                {
                    MultiNodeMap.Add(i);
                }
            }

            var isEnd = false;
            var steps = 0;
            var finished = 0;
            while (!isEnd)
            {
                for (var i = 0; i < Directions.Length; i++)
                {
                    for (var j = 0; j < MultiNodeMap.Count; j++)
                    {
                        var index = 0;
                        if (Directions[i] == 'L')
                        {
                            index = Map.FindIndex(a => a.Position.ToLower().Contains(MultiNodeMap[j].Left.ToLower()));
                        }
                        else
                        {
                            index = Map.FindIndex(a => a.Position.ToLower().Contains(MultiNodeMap[j].Right.ToLower()));
                        }
                        MultiNodeMap[j] = Map[index];
                        if (MultiNodeMap[j].Position.ToLower().EndsWith('z'))
                        {
                            finished++;
                        }
                    }
                    steps++;
                    //foreach (var ix in MultiNodeMap)
                    //{
                    //    Log.Information(ix.Position.ToString());
                    //}
                    //Log.Information("");
                    isEnd = MultiNodeMap.All(a => a.Position.ToLower().EndsWith('z'));

                    if (!isEnd && i == Directions.Length - 1)
                    {
                        i = -1;
                    }
                }
            }
            Result2 = steps;

            PrintResults(Result2);
        }
    }
}
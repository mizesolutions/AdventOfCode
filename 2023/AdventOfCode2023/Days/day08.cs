
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

            //foreach (var i in Map)
            //{
            //    if (i.Position.ToLower().EndsWith("a"))
            //    {
            //        MultiNodeMap.Add(i);
            //    }
            //}

            //var stepList = new List<int>();
            //var isEnd = false;
            //for (var j = 0; j < MultiNodeMap.Count; j++)
            //{
            //    Console.WriteLine(" ");
            //    Console.WriteLine(MultiNodeMap[j].Position);

            //    var steps = 0;
            //    for (var i = 0; i < Directions.Count(); i++)
            //    {
            //        var index = 0;
            //        if (Directions[i] == 'L')
            //        {
            //            index = Map.FindIndex(a => a.Position.ToLower().Contains(MultiNodeMap[j].Left.ToLower()));
            //        }
            //        else
            //        {
            //            index = Map.FindIndex(a => a.Position.ToLower().Contains(MultiNodeMap[j].Right.ToLower()));
            //        }
            //        MultiNodeMap[j] = Map[index];
            //        steps++;

            //        if (i == Directions.Count() - 1)
            //        {
            //            i = -1;
            //        }

            //        if (MultiNodeMap[j].Position.ToLower().EndsWith('z'))
            //        {
            //            stepList.Add(steps);
            //            Console.WriteLine(MultiNodeMap[j].Position);
            //            i = Directions.Count() * 800;
            //        }
            //    }
            //    Console.WriteLine(stepList[j]);
            //}
            //Console.WriteLine(" ");
            //foreach (var n in stepList)
            //{
            //    Console.WriteLine(n);
            //}

            //var resultList = new List<long>()
            //{
            //    0,0,0,0,0,0
            //};

            //while (!isEnd)
            //{
            //    for (var x = 0; x < stepList.Count(); x++)
            //    {
            //        var temp = (resultList[x] + stepList[x]);
            //        resultList[x] = temp;
            //    }
            //    isEnd = !resultList.Any(o => o != resultList[0]);
            //}

            //Result2 = steps;
            ChatGPT();

            PrintResults(Result2);
        }


        private void ChatGPT()
        {
            // Define the map as a dictionary where the key is the node name
            // and the value is a tuple of left and right connections.
            Dictionary<string, (string left, string right)> map = new Dictionary<string, (string, string)>
            {
                { "11A", ("11B", "XXX") },
                { "11B", ("XXX", "11Z") },
                { "11Z", ("11B", "XXX") },
                { "22A", ("22B", "XXX") },
                { "22B", ("22C", "22C") },
                { "22C", ("22Z", "22Z") },
                { "22Z", ("22B", "22B") },
                { "XXX", ("XXX", "XXX") }
            };

            // Find all nodes that end with A
            List<string> startingNodes = new List<string>();
            foreach (var node in map.Keys)
            {
                if (node.EndsWith("A"))
                {
                    startingNodes.Add(node);
                }
            }

            // Initialize the set of current nodes with starting nodes
            HashSet<string> currentNodes = new HashSet<string>(startingNodes);

            // Initialize the step counter
            int steps = 0;

            // Simulate the navigation until all nodes end with Z
            while (!currentNodes.All(node => node.EndsWith("Z")))
            {
                // Update the set of current nodes based on left and right connections
                HashSet<string> newNodes = new HashSet<string>();
                foreach (var currentNode in currentNodes)
                {
                    var connections = map[currentNode];
                    newNodes.Add(connections.left);
                    newNodes.Add(connections.right);
                }

                currentNodes = newNodes;
                steps++;
            }

            // Output the result
            Console.WriteLine($"It takes {steps} steps before you're only on nodes that end with Z.");
        }
    }
}

namespace AdventOfCode2021.Days
{
    public class Day02 : BaseDay
    {
        private int Xposition { get; set; }
        private int Yposition { get; set; }
        private int Aim { get; set; }

        public Day02(string day, bool hasInput) : base(day, hasInput)
        {
            Xposition = 0;
            Yposition = 0;
            Aim = 0;
        }

        public void PuzzleOne()
        {
            PrintCurrentMethod();
            Result1 = 0;
            foreach (var s in FileInput)
            {
                var split = s.Split(" ");
                switch (split[0])
                {
                    case "forward":
                        Xposition += int.Parse(split[1]);
                        break;
                    case "down":
                        Yposition += int.Parse(split[1]);
                        break;
                    case "up": 
                        Yposition -= int.Parse(split[1]);
                        break;
                    default:
                        break;
                }
            }
            Result1 = Xposition * Yposition;
            PrintResults(Result1);
        }

        public void PuzzleTwo()
        {
            PrintCurrentMethod();
            Result2 = 0;
            Xposition = 0;
            Yposition = 0;
            Aim = 0;
            foreach (var s in FileInput)
            {
                var split = s.Split(" ");
                switch (split[0])
                {
                    case "forward":
                        Xposition += int.Parse(split[1]);
                        Yposition += Aim * int.Parse(split[1]);
                        break;
                    case "down":
                        Aim += int.Parse(split[1]);
                        break;
                    case "up":
                        Aim -= int.Parse(split[1]);
                        break;
                    default:
                        break;
                }
            }
            Result2 = Xposition * Yposition;
            PrintResults(Result2);
        }
    }
}

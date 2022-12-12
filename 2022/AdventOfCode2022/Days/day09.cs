using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day09 : BaseDay
    {
        //public Point Head { get; set; }
        //public Point Tail { get; set; }
        //public HashSet<Point> TailPoints { get; set; }
        //public Double Distance { get; set; }

        public Day09 (string day, bool hasInput) : base(day, hasInput)
        {
            //Head = new Point(0,0);
            //Tail = new Point(0,0);
            //TailPoints = new();
            //TailPoints.Add(Tail);
            //Distance = 0;
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            //MoveRope();
            //Result1 = TailPoints.Count;
            Result1 = CountPositions(2);
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            Result2 = CountPositions(10);
            PrintResults(Result2);
        }

        // My solution worked for the example input but not my puzzle input.
        // The result was lower than the correct answer by half. I don't know
        // where the flaw is.
        //private void MoveRope()
        //{
        //    foreach(var m in FileInput)
        //    {
        //        int x1 = 0;
        //        int y1 = 0;
        //        int x2 = 0;
        //        int y2 = 0;
        //        switch (m[0].ToString())
        //        {
        //            case "U":
        //                y1 = Head.Y + int.Parse(m[2].ToString());
        //                y2 = y1 - 1;
        //                x1 = Head.X;
        //                x2 = Tail.X;
        //                break;
        //            case "D":
        //                y1 = Head.Y - int.Parse(m[2].ToString());
        //                y2 = y1 + 1;
        //                x1 = Head.X;
        //                x2 = Tail.X;
        //                break;
        //            case "L":
        //                x1 = Head.X - int.Parse(m[2].ToString());
        //                x2 = x1 + 1;
        //                y1 = Head.Y;
        //                y2 = Tail.Y;
        //                break;
        //            case "R":
        //                x1 = Head.X + int.Parse(m[2].ToString());
        //                x2 = x1 - 1;
        //                y1 = Head.Y;
        //                y2 = Tail.Y;
        //                break;
        //        }
        //        MoveRope(x1, y1, x2, y2, m[0].ToString());
        //    }
        //}

        //private void MoveRope(int x1, int y1, int x2, int y2, string move)
        //{
        //    Head = new(x1, y1);
        //    MoveTail(x2, y2, move);
        //}

        //private void MoveTail(int x2, int y2, string m)
        //{
        //    if (MoveTheTail())
        //    {
        //        switch (m)
        //        {
        //            case "U":
        //            case "D":
        //                MoveCache(Math.Abs(y2 - Tail.Y), m);
        //                break;
        //            case "L":
        //            case "R":
        //                MoveCache(Math.Abs(x2 - Tail.X), m);
        //                break;
        //        }
        //    }
        //}

        //private void MoveCache(int diff, string direction)
        //{
        //    if(direction == "U")
        //    {
        //        if (Head.X - Tail.X != 0)
        //        {
        //            Tail = new(Head.X, Tail.Y);
        //        }
        //        for (var i = 1; i <= diff; i++)
        //        {
        //            TailPoints.Add(new(Tail.X, Tail.Y + i));
        //        }
        //        Tail = new(Tail.X, Head.Y - 1);
        //    }
        //    if (direction == "D")
        //    {
        //        if (Head.X - Tail.X != 0)
        //        {
        //            Tail = new(Head.X, Tail.Y);
        //        }
        //        for (var i = 1; i <= diff; i++)
        //        {
        //            TailPoints.Add(new(Tail.X, Tail.Y - i));
        //        }
        //        Tail = new(Tail.X, Head.Y + 1);
        //    }
        //    if (direction == "L")
        //    {
        //        if (Head.Y - Tail.Y != 0)
        //        {
        //            Tail = new(Tail.X, Head.Y);
        //        }
        //        for (var i = 1; i <= diff; i++)
        //        {
        //            TailPoints.Add(new(Tail.X - i, Tail.Y));
        //        }
        //        Tail = new(Head.X + 1, Tail.Y);
        //    }
        //    if (direction == "R")
        //    {
        //        if (Head.Y - Tail.Y != 0)
        //        {
        //            Tail = new(Tail.X, Head.Y);
        //        }
        //        for (var i = 1; i <= diff; i++)
        //        {
        //            TailPoints.Add(new(Tail.X + i, Tail.Y));
        //        }
        //        Tail = new(Head.X - 1, Tail.Y);
        //    }

        //}

        //private bool MoveTheTail()
        //{
        //    Distance = Math.Floor(Math.Sqrt(Math.Pow((Head.X - Tail.X),2) + Math.Pow((Head.Y - Tail.Y),2)));
        //    return Distance > 1;
        //}

        // It should be obvious that I got stuck and looked this up.
        // This is not my code.
        private int CountPositions(int knots)
        {
            HashSet<(int x, int y)> positions = new();
            (int x, int y)[] rope = new (int x, int y)[knots];

            foreach (string line in FileInput)
            {
                int amount = int.Parse(line[2..]);
                int xd = line[0] switch { 'R' => 1, 'L' => -1, _ => 0 };
                int yd = line[0] switch { 'U' => 1, 'D' => -1, _ => 0 };

                while (amount-- > 0)
                {
                    rope[0] = (rope[0].x + xd, rope[0].y + yd);

                    for (int i = 1; i < rope.Length; i++)
                    {
                        int diffX = rope[i - 1].x - rope[i].x;
                        int diffY = rope[i - 1].y - rope[i].y;
                        if (Math.Abs(diffX) > 1 || Math.Abs(diffY) > 1)
                        {
                            rope[i] = (rope[i].x + Math.Sign(diffX), rope[i].y + Math.Sign(diffY));
                        }
                    }

                    positions.Add(rope[^1]);
                }
            }
            return positions.Count;
        }
    }
}
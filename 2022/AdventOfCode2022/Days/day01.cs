using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day01 : BaseDay
    {
        List<Elf> Elves { get; set; }

        public Day01(string day, bool hasInput) : base(day, hasInput)
        {
            Result1 = 0;
            Result2 = 0;
            Elves = new List<Elf>();
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            var tempSum = 0;
            var id = 1;
            foreach (var i in FileInput)
            {
                if (!string.IsNullOrEmpty(i))
                {
                    tempSum += int.Parse(i);
                }
                else
                {
                    Elves.Add(new Elf() { Calories = tempSum, Id = id });
                    id++;
                    tempSum = 0;
                }
            }
            Elves = Elves.OrderByDescending(el => el.Calories).ToList();
            Result1 = Elves.First().Calories;
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            Result2 = Elves[0].Calories + Elves[1].Calories + Elves[2].Calories;
            PrintResults(Result2);
        }
    }

    internal class Elf
    {
        public int Calories { get; set; }
        public int Id { get; set; }
    }
}
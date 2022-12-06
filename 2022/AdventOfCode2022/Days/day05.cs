using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day05 : BaseDay
    {
        private List<Stack<string>> CratesOne { get; }
        private List<Stack<string>> CratesTwo { get; }
        private List<List<int>> Moves { get; }
        private StringBuilder ResultOneString { get; set; }
        private StringBuilder ResultTwoString { get; set; }

        public Day05 (string day, bool hasInput) : base(day, hasInput)
        {
            ResultOneString = new();
            ResultTwoString = new();
            String[] moveDelimiters = { "move ", "from ", "to " };
            CratesOne = new();
            CratesTwo = new();
            Moves = new();
            foreach (var i in FileInput)
            {
                if (i.Contains(',') || (!string.IsNullOrEmpty(i) && i.Length == 1))
                {
                    CratesOne.Add(new Stack<string>(i.Split(',')));
                    CratesTwo.Add(new Stack<string>(i.Split(',')));
                }
                else if (string.IsNullOrEmpty(i))
                {
                    continue;
                }
                else
                {
                    var temp = i.Split(moveDelimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
                    Moves.Add(temp.Select(int.Parse).ToList());
                }
            }
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            foreach (var m in Moves)
            {
                MoveCratesOne(m);
                MoveCratesTwo(m);
            }
            TopCratesOne();
            TopCratesTwo();
            PrintResults(ResultOneString);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(ResultTwoString);
        }

        private void MoveCratesOne(List<int> moves)
        {
            for(var i = 0; i < moves[0]; i++)
            {
                CratesOne[moves[2] - 1].Push(CratesOne[moves[1] - 1].Pop());
            }
        }

        private void TopCratesOne()
        {
            foreach (var c in CratesOne)
            {
                ResultOneString.Append(c.Pop());
            }
        }

        private void MoveCratesTwo(List<int> moves)
        {
            Stack<string> tempStack = new();
            for (var i = 0; i < moves[0]; i++)
            {
                tempStack.Push(CratesTwo[moves[1] - 1].Pop());
            }
            foreach(var c in tempStack)
            {
                CratesTwo[moves[2] - 1].Push(c);
            }
        }

        private void TopCratesTwo()
        {
            foreach (var c in CratesTwo)
            {
                ResultTwoString.Append(c.Pop());
            }
        }
    }
}
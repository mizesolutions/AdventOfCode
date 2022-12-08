using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day06 : BaseDay
    {
        public Queue<char> CharPacketQueue { get; set; }
        public Queue<char> CharMessageQueue { get; set; }
        public string Signal { get; set; }

        public Day06(string day, bool hasInput) : base(day, hasInput)
        {
            CharPacketQueue = new();
            CharMessageQueue = new();
            Signal = FileInput[0];
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            for (var i = 0; i < Signal.Length; i++)
            {
                Result1 = i + 1;
                if (CharPacketQueue.Count == 4)
                {
                    CharPacketQueue.Dequeue();
                    CharPacketQueue.Enqueue(Signal[i]);
                    if (IsStartPacketMarker())
                    {
                        i = Signal.Length;
                    }
                }
                else
                {
                    CharPacketQueue.Enqueue(Signal[i]);
                }
            }
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            for (var i = 0; i < Signal.Length; i++)
            {
                Result2 = i + 1;
                if (CharMessageQueue.Count == 14)
                {
                    CharMessageQueue.Dequeue();
                    CharMessageQueue.Enqueue(Signal[i]);
                    if (IsStartMessageMarker())
                    {
                        i = Signal.Length;
                    }
                }
                else
                {
                    CharMessageQueue.Enqueue(Signal[i]);
                }
            }
            PrintResults(Result2);
        }

        private bool IsStartPacketMarker()
        {
            return !(CharPacketQueue.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).Any());
        }

        private bool IsStartMessageMarker()
        {
            return !(CharMessageQueue.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).Any());
        }
    }
}
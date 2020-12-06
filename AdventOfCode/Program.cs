using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public partial class Model
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public char Char { get; set; }
            public string Pw { get; set; }
            public int Count { get; set; }
            public bool Pass { get; set; }
            public override string ToString()
            {
                return $"\r\nMODEL\r\n" +
                       $"Min: {Min}\r\n" +
                       $"Max: {Max}\r\n" +
                       $"Char: {Char}\r\n" +
                       $"Pw: {Pw}\r\n" +
                       $"Count: {Count}\r\n" +
                       $"Pass: {Pass}\r\n";
            }
        }

        static void Main(string[] args)
        {
           
            if(args.Length > 0)
            {
                string[] lines = File.ReadAllLines(args[0]);
                //PrintArray(lines);
                List<Model> pwList = new List<Model>();
                for(var i = 0; i < lines.Length; i ++)
                {
                    string[] subs = lines[i].Split(' ');
                    string[] minMax = subs[0].Split('-');
                    string[] charCheck = subs[1].Split(':');
                    var temp = new Model
                    {
                        Min = int.Parse(minMax[0]),
                        Max = int.Parse(minMax[1]),
                        Char = char.Parse(charCheck[0]),
                        Pw = subs[2],
                        Count = 0,
                        Pass = false
                    };
                    temp.Pass = (temp.Pw[temp.Min-1].Equals(temp.Char) && !temp.Pw[temp.Max-1].Equals(temp.Char)) ||
                                    (!temp.Pw[temp.Min - 1].Equals(temp.Char) && temp.Pw[temp.Max - 1].Equals(temp.Char));
                    if (temp.Pass)
                    {
                        pwList.Add(temp);
                    }
                }
                foreach(var val in pwList)
                {
                    Console.WriteLine(val);
                }
                Console.WriteLine($"Result: {pwList.Count}\r\n");


                //int[] myInts = Array.ConvertAll(lines, s => int.Parse(s));
                //Array.Sort(myInts);
                //foreach (var val in myInts)
                //{
                //    Console.WriteLine(val);
                //}

                //for(var p1 = 0; p1 < myInts.Length; p1++)
                //{
                //    for(var p2 = p1; p2 < myInts.Length; p2++)
                //    {
                //        for (var p3 = p1; p3 < myInts.Length; p3++)
                //        {
                //            if ((myInts[p1] + myInts[p2] + myInts[p3]) == 2020)
                //            {
                //                Console.WriteLine($"Result: {myInts[p1] * myInts[p2] * myInts[p3]}");
                //            }
                //        }
                //    }
                //}
            }
            else
            {
                Console.WriteLine("Must supply an inputfile");
            }
        }

        private static void PrintArray(Array list)
        {
            foreach (var val in list)
            {
                Console.WriteLine(val);
            }
        }

    }
}

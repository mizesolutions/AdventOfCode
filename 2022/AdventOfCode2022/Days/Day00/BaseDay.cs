using AdventOfCode2022.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Days.Day00
{
    public class BaseDay
    {
        public RenderInput Input { get; set; }
        public List<string> FileInput { get; set; }
        public int Result1 { get; set; }
        public int Result2 { get; set; }

        public BaseDay(string day, bool hasInput)
        {
            if (hasInput)
            {
                try
                {
                    Input = new RenderInput(day);
                    FileInput = Input.FileToList();
                    Result1 = 0;
                    Result2 = 0;
                }
                catch (FileNotFoundException fe)
                {
                    Console.WriteLine(fe.Message);
                    Environment.Exit(0);
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void PrintCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            Console.WriteLine($"  {sf.GetMethod().Name}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void PrintCurrentClass()
        {
            Console.WriteLine($"--==<  {this.GetType().Name}  >==--");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void PrintResults(int results)
        {
            Console.WriteLine($"      Result: {results}\r\n");
        }
    }
}

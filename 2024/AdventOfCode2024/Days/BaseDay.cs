using AdventOfCode2024.Infrastructure.Services;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdventOfCode2024.Days
{
    public abstract class BaseDay
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

        public void RunPuzzles()
        {
            PrintCurrentClass();
            PuzzleOne();
            PuzzleTwo();
        }

        public abstract void PuzzleOne();
        public abstract void PuzzleTwo();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void PrintCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            Console.WriteLine($"  {sf.GetMethod().Name}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void PrintCurrentClass()
        {
            Console.WriteLine($"--==<  {GetType().Name}  >==--");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void PrintResults<T>(T results)
        {
            Console.WriteLine($"      Result: {results}\r\n");
        }
    }
}

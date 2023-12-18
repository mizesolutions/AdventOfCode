using AdventOfCode2023.Infrastructure.Services;
using System.Diagnostics;

namespace AdventOfCode2023
{
    public class Program
    {
        static int Main(string[] args)
        {
            var watch = new Stopwatch();

            watch.Start();
            try
            {
                if (args != null && args.Length > 0)
                {
                    string? day;
                    bool isInput = true;
                    if (args.Length == 2 && args[0].Contains("-n"))
                    {
                        isInput = false;
                        day = args[1];
                    }
                    else
                    {
                        day = args[0];
                    }
                    try
                    {
                        var _dayrunner = new DayRunner(day, isInput);
                        var _day = _dayrunner.RunDay();
                        _day.RunPuzzles();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine($"No args found - \r\nUsage: \r\n\tAdventOfCode2023 day##   normal operation\r\n\tAdventOfCode2023 -n day##   to run with no input");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
            finally
            {
                watch.Stop();
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            }

        }
    }
}
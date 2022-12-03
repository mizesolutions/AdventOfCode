using AdventOfCode2019.Infrastructure.Services;

namespace AdventOfCode2019
{
    public class Program
    {
        static int Main(string[] args)
        {
            bool isInput = true;

            if (args != null && args.Length > 0)
            {
                string? day;
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
                    Console.WriteLine($"Error: {ex.Message}");
                    return 0;
                }
            }
            else
            {
                Console.WriteLine($"No args found - \r\nUsage: \r\n\tAdventOfCode2022 day##   normal operation\r\n\tAdventOfCode20221 -n day##   to run with no input");
                return 0;
            }
        }
    }
}
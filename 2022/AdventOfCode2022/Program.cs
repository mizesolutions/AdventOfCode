
using AdventOfCode2022.Infrastructure.Services;

namespace AdventOfCode2022
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
                } catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return 0;
                }
            }
            else
            {
                Console.WriteLine($"No args found - \r\nUsage: \r\n\taoc21 day##   normal operation\r\n\taoc21 -n day##   to run with no input");
                return 0;
            }
        }
    }
}
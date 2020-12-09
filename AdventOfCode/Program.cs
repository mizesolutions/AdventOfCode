using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Program
    {
        static void Main(string[] args)
        {
           
            if(args.Length > 0)
            {
                //List<string> input = File.ReadAllLines(args[0]).ToList();
                var input = File.ReadAllText(args[0]);

                //Day4(lines);
                //Day4_1_2(input);
                //Day5_1(input);
                Day6_1(input);
                Day6_2(input);
                //Console.WriteLine($"Resutl: {Day3(lines, 1, 1) * Day3(lines, 3, 1) * Day3(lines, 5, 1) * Day3(lines, 7, 1) * Day3(lines, 1, 2)}");
                
            }
            else
            {
                Console.WriteLine("Must supply an inputfile");
            }
        }

        private static void PrintArray<T>(List<T> list)
        {
            foreach (var val in list)
            {
                Console.WriteLine(val);
            }
            Console.WriteLine($"Record Count: {list.Count}");
        }

        private static int Day3<T>(List<T> list, int r, int d)
        {
            var item = list.First().ToString();
            Console.WriteLine($"Line Count: {item.Length}");
            Console.WriteLine($"List Count: {list.Count}");

            T[] theHill = new T[list.Count];
            theHill = list.ToArray();
            var treeCount = 0;
            var i = 0;
            var j = 0;
            while(j < theHill.Length - 1)
            {
                i += r;
                j += d;
                if (i > theHill[j].ToString().Length - 1)
                {
                    i -= theHill[j].ToString().Length;
                }
                var row = theHill[j].ToString();
                var tree = row[i];
                //Console.WriteLine($"{tree}");
                if (tree.Equals('#'))
                {
                    treeCount++;
                }
            }
            Console.WriteLine($"Result: {treeCount}\r\n");
            return treeCount;
        }

        private static string ReplaceAt(this string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        public partial class Model2
        {
            public string byr { get; set; }
            public string iyr { get; set; }
            public string eyr { get; set; }
            public string hgt { get; set; }
            public string hgtType { get; set; }
            public string hcl { get; set; }
            public string ecl { get; set; }
            public string pid { get; set; }
            public string cid { get; set; }
            public bool Pass { get; set; }
            public override string ToString()
            {
                return $"\r\nMODEL2\r\n" +
                       $"byr:{byr}\r\n" +
                       $"iyr:{iyr}\r\n" +
                       $"eyr:{eyr}\r\n" +
                       $"hgt:{hgt}\r\n" +
                       $"hcl:{hcl}\r\n" +
                       $"ecl:{ecl}\r\n" +
                       $"pid:{pid}\r\n" +
                       $"cid:{cid}\r\n" +
                       $"Pass: {Pass}\r\n";
            }
        }

        private static void Day4_1_2(string input)
        {
            //Console.WriteLine(input);
            int validPassports = 0;
            Dictionary<string, bool> fieldIds = new Dictionary<string, bool>()
            {
                {"byr", true},
                {"iyr", true},
                {"eyr", true},
                {"hgt", true},
                {"hcl", true},
                {"ecl", true},
                {"pid", true},
                {"cid", false}
            };
            int expectedReqFieldCount = fieldIds.Count(x => x.Value);
            var passports = input.Replace("\r", "").Replace("\n\n", "@").Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"Found {passports.Length:N0} passports");
            List<Model2> ppList = new List<Model2>();
            foreach(string passport in passports)
            {
                var temp = new Model2();
                string[] parts = passport.Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var fields = parts.Select(x => x.Split(':')).Select(x => new { key = x[0], value = x[1] }).ToList();
                int reqFieldCount = 0, optFieldCount = 0;
                foreach (var field in fields)
                {
                    switch (field.key)
                    {
                        case "byr":
                            temp.byr = field.value;
                            break;
                        case "iyr":
                            temp.iyr = field.value;
                            break;
                        case "eyr":
                            temp.eyr = field.value;
                            break;
                        case "hgt":
                            temp.hgt = field.value.Substring(0, field.value.Length - 2);
                            temp.hgtType = field.value.Substring(field.value.Length - 2);
                            break;
                        case "hcl":
                            temp.hcl = field.value;
                            break;
                        case "ecl":
                            temp.ecl = field.value;
                            break;
                        case "pid":
                            temp.pid = field.value;
                            break;
                        case "cid":
                            temp.cid = field.value;
                            break;
                        default:
                            Console.WriteLine($"Not Found: {field.key}");
                            break;
                    }

                    if (fieldIds.TryGetValue(field.key, out bool required))
                    {
                        if (required) ++reqFieldCount;
                        else ++optFieldCount;
                    }
                    else
                    {
                        Console.WriteLine($"We don't recogize field '{field.key}'");
                    }
                }
                if (reqFieldCount == expectedReqFieldCount) ++validPassports;
                temp.Pass = TestSet(temp);
                ppList.Add(temp);
            }
            Console.WriteLine($"Valid passports - Part 1: {validPassports:N0}");
            Console.WriteLine($"Valid passports - Part 2: {ppList.Count(p => p.Pass):N0}");
        }

        private static bool TestSet(Model2 set)
        {
            return !string.IsNullOrEmpty(set.byr) && int.TryParse(set.byr, out _) && int.Parse(set.byr) >= 1920 && int.Parse(set.byr) <= 2002 &&
                   !string.IsNullOrEmpty(set.iyr) && int.TryParse(set.iyr, out _) && int.Parse(set.iyr) >= 2010 && int.Parse(set.iyr) <= 2020 &&
                   !string.IsNullOrEmpty(set.eyr) && int.TryParse(set.eyr, out _) && int.Parse(set.eyr) >= 2020 && int.Parse(set.eyr) <= 2030 &&
                   !string.IsNullOrEmpty(set.hgt) && int.TryParse(set.hgt, out _) && ((set.hgtType == "cm" && int.Parse(set.hgt) >= 150 && int.Parse(set.hgt) <= 193) || (set.hgtType == "in" && int.Parse(set.hgt) >= 59 && int.Parse(set.hgt) <= 76)) &&
                   !string.IsNullOrEmpty(set.hcl) && set.hcl[0] == '#' && set.hcl.Length == 7 && TestHcl(set.hcl) &&
                   !string.IsNullOrEmpty(set.ecl) && set.ecl.Length == 3 && TestEcl(set.ecl) &&
                   !string.IsNullOrEmpty(set.pid) && int.TryParse(set.pid, out _) && TestPid(set.pid);
        }

        private static bool TestHcl(string hcl)
        {
            var pattern = "^#([0-9a-f]{6})$";
            Regex rg = new Regex(pattern);
            return rg.IsMatch(hcl);
        }

        private static bool TestEcl(string ecl)
        {
            return ecl == "amb" || ecl == "blu" || ecl == "brn" || ecl == "gry" || ecl == "grn" || ecl == "hzl" || ecl == "oth";
        }

        private static bool TestPid(string pid)
        {
            var pattern = "^([0-9]{9})$";
            Regex rg = new Regex(pattern);
            return rg.IsMatch(pid);
        }

        private static void Day5_1(List<string> input)
        {
            /*
                FBFBBFFRLR
             
                Start by considering the whole range, rows 0 through 127.
                F means to take the lower half, keeping rows 0 through 63.
                B means to take the upper half, keeping rows 32 through 63.
                F means to take the lower half, keeping rows 32 through 47.
                B means to take the upper half, keeping rows 40 through 47.
                B keeps rows 44 through 47.
                F keeps rows 44 through 45.
                The final F keeps the lower of the two, row 44.

                Start by considering the whole range, columns 0 through 7.
                R means to take the upper half, keeping columns 4 through 7.
                L means to take the lower half, keeping columns 4 through 5.
                The final R keeps the upper of the two, column 5.

                Every seat also has a unique seat ID: multiply the row by 8, 
                then add the column. In this example, the seat has ID 44 * 8 + 5 = 357

                BFFFBBFRRR: row 70, column 7, seat ID 567.
                FFFBBBFRRR: row 14, column 7, seat ID 119.
                BBFFBBFRLL: row 102, column 4, seat ID 820.
            */
            int rowMax = 127;
            int rowMin = 0;
            int colMax = 7;
            int colMin = 0;
            int maxId = 0;
            var seatId = new int[817];
            foreach (var e in input)
            {
                int r = FindPosition(rowMax, rowMin, e.Substring(0, 7).ToString());
                int c = FindPosition(colMax, colMin, e.Substring(7).ToString());
                int s = (r * 8) + c;
                maxId = s > maxId ? s : maxId;
                seatId[s-32]=s;
                //Console.WriteLine($"Row: {r} Col: {c} Seat Id: {s}\r\n");
            }
            Console.WriteLine($"Max Seat Id: {maxId}\r\n");
            int mySeat = Array.FindIndex(seatId, i => i == 0)+32;
            Console.WriteLine($"My Seat Id: {mySeat}\r\n");
            foreach (var e in seatId)
            {
                Console.WriteLine($"Index: {e-32} Seat Id: {e}");
            }

        }

        private static void Print(int max, int min)
        {
            Console.WriteLine($"Range: {min} - {max}");
        }

        private static int FindPosition(int max, int min, string id)
        {
            if (min == max)
            {
                return min;
            }
            else if (id[0].Equals('F') || id[0].Equals('L'))
            {
                max = max - (int)Math.Floor((double)(max - min) / 2) - 1;
                return FindPosition(max, min, id.Substring(1));
            }
            else
            {
                min = max - (int)Math.Ceiling((double)(max - min) / 2) + 1;
                return FindPosition(max, min, id.Substring(1));
            }
        }

        private static void Day6_1(string input)
        {
            var groups = input.Replace("\n\n", "@").Replace("\n", "").Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;
            foreach (var group in groups)
            {
                sum += group.Distinct().Count();
            }
            Console.WriteLine($"\r\n\r\nTotal Sum: {sum}\r\n\r\n");
        }

        private static void Day6_2(string input)
        {
            var groups = input.Replace("\n\n", "@").Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;
            var i = 0;
            foreach (var group in groups)
            {
                var sub = group.Split("\n");
                var index = new int[27];
                i++;
                foreach(var s in sub)
                {
                    foreach(var c in s)
                    {
                        index[c - 96] ++;
                    }
                }
                sum += index.Count(i => i == sub.Length);
            }
            Console.WriteLine($"\r\n\r\nTotal Sum: {sum}\r\n\r\n");
        }

    }
}

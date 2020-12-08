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
                //List<string> lines = File.ReadAllLines(args[0]).ToList();
                var input = File.ReadAllText(args[0]);
                //Console.WriteLine($"Records: {lines.Count(p => p.Equals(""))}\r\n");
                //PrintArray(lines);
                //Day4(lines);
                Day4_1_2(input);
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


    }
}

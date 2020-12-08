using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                    if(fieldIds.TryGetValue(field.key, out bool required))
                    {
                        if (required) ++reqFieldCount;
                        else ++optFieldCount;
                    }
                    else
                    {
                        Console.WriteLine($"We don't recogise field '{field.key}'");
                    }
                }
                if (reqFieldCount == expectedReqFieldCount) ++validPassports;
            }
            Console.WriteLine($"Valid passports: {validPassports:N0}");
        }

        private static void Day4<T>(List<T> list)
        {
            Console.WriteLine($"::: Start Day4 func :::");
            List<string> set = new List<string>();
            foreach(var val in list)
            {
                if (string.IsNullOrEmpty(val.ToString()))
                {
                    set.Add("-");
                }
                else
                {
                    if (val.ToString().Contains(" "))
                    {
                        string[] temp = val.ToString().Split(" ");
                        foreach (var s in temp)
                        {
                            set.Add(s);
                        }
                    }
                    else
                    {
                        set.Add(val.ToString());
                    }
                }
            }
            var modelSet = BuildSet(set);
            var checkCount = modelSet.Count(p => p.Pass == true);
            var falseCount = modelSet.Count(p => p.Pass == false);
            foreach (var e in modelSet)
            {
                if (!e.Pass)
                {
                    Console.WriteLine(e);
                }
            }
            Console.WriteLine($"Set Count Post: {modelSet.Count}\r\n");
            Console.WriteLine($"True Result: {checkCount}\r\n");
            Console.WriteLine($"False Result: {falseCount}\r\n");
            Console.WriteLine($"::: End Day4 func :::");
        }

        private static List<Model2> BuildSet(List<string> list)
        {
            Console.WriteLine($"::: Start BuildSet func :::");
            List<Model2> set = new List<Model2>();
            var tempModel2 = new Model2();
            foreach (var val in list)
            {
                if (val.ToString().Equals("-"))
                {
                    tempModel2.Pass = TestSet(tempModel2);
                    set.Add(tempModel2);
                    Console.WriteLine($"temp: {tempModel2}");
                    tempModel2 = new Model2();
                }
                else
                {
                    var id = val.ToString().Substring(0, 3);
                    switch (id)
                    {
                        case "byr":
                            tempModel2.byr = val.ToString().Substring(4);
                            break;
                        case "iyr":
                            tempModel2.iyr = val.ToString().Substring(4);
                            break;
                        case "eyr":
                            tempModel2.eyr = val.ToString().Substring(4);
                            break;
                        case "hgt":
                            tempModel2.hgt = val.ToString().Substring(4);
                            break;
                        case "hcl":
                            tempModel2.hcl = val.ToString().Substring(4);
                            break;
                        case "ecl":
                            tempModel2.ecl = val.ToString().Substring(4);
                            break;
                        case "pid":
                            tempModel2.pid = val.ToString().Substring(4);
                            break;
                        case "cid":
                            tempModel2.cid = val.ToString().Substring(4);
                            break;
                        default:
                            Console.WriteLine($"Not Found: {id}");
                            break;
                    }
                }
            }
            //PrintArray(set);
            Console.WriteLine($"::: End BuildSet func :::");
            return set;
        }

        private static bool TestSet(Model2 set)
        {
            return !string.IsNullOrEmpty(set.byr) && !string.IsNullOrEmpty(set.iyr) &&
                   !string.IsNullOrEmpty(set.eyr) && !string.IsNullOrEmpty(set.hgt) &&
                   !string.IsNullOrEmpty(set.hcl) && !string.IsNullOrEmpty(set.ecl) &&
                   !string.IsNullOrEmpty(set.pid);
        }


    }
}

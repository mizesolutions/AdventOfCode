﻿using AdventOfCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Program
    {
        public static HashSet<string> Bags { get; set; }
        public static TreeNode<string> Root { get; set; }

        static void Main(string[] args)
        {

            if (args.Length > 0)
            {
                //List<string> input = File.ReadAllLines(args[0]).ToList();
                var input = File.ReadAllText(args[0]);
                string[] splitInput = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                //Day4(lines);
                //Day4_1_2(input);
                //Day5_1(input);
                //Day6_1(input);
                //Day6_2(input);
                //Day7_1(input);
                //Day7_1_2(input);
                //Day8_1(input);
                //Day9_1(input, args[0]);
                //Day10(args[0]);
                //Day11(input);
                //Day12(splitInput);
                Day13(splitInput);
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
            Console.WriteLine($"Record Count: {list.Count}\r\n");
        }

        #region Day 3
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
            while (j < theHill.Length - 1)
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
        #endregion Day 3

        #region Day 4
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
            foreach (string passport in passports)
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
        #endregion Day 4

        #region Day 5
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
                seatId[s - 32] = s;
                //Console.WriteLine($"Row: {r} Col: {c} Seat Id: {s}\r\n");
            }
            Console.WriteLine($"Max Seat Id: {maxId}\r\n");
            int mySeat = Array.FindIndex(seatId, i => i == 0) + 32;
            Console.WriteLine($"My Seat Id: {mySeat}\r\n");
            foreach (var e in seatId)
            {
                Console.WriteLine($"Index: {e - 32} Seat Id: {e}");
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
        #endregion Day 5

        #region Day 6
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
                foreach (var s in sub)
                {
                    foreach (var c in s)
                    {
                        index[c - 96]++;
                    }
                }
                sum += index.Count(i => i == sub.Length);
            }
            Console.WriteLine($"\r\n\r\nTotal Sum: {sum}\r\n\r\n");
        }
        #endregion Day 6

        #region Day 7
        private static void Day7_1(List<string> input)
        {
            var bag = "shiny gold";
            //Bags = new HashSet<string>();
            Root = new TreeNode<string>(bag);
            //Bags.Add(RecRuns(input, bag));
            //Bags.RemoveWhere(x => x.Equals(""));
            //Console.WriteLine($"Bag Count: {Bags.Count}");
            Root.AddChild(RecRuns(input, Root.Value));
            var list = Root.Flatten().ToList();
            list.Sort();
            PrintArray(list);
            var result = list.Where(s => !s.Equals("")).Select(s => s).Distinct().ToList();
            PrintArray(result);
            Console.WriteLine($"Bag Count: {result.Count}");
        }

        private static string RecRuns(List<string> input, string bag)
        {
            if (string.IsNullOrEmpty(bag))
            {
                return "";
            }
            else
            {
                var run = GetBags(input, bag);
                //PrintArray(run);
                foreach (var s in run)
                {
                    Root.AddChild(SubString(s));
                    RecRuns(input, SubString(s));
                }
                return RecRuns(input, "");
            }
        }

        private static List<string> GetBags(List<string> input, string type)
        {
            return input.Where(s => !s.Contains("no other bags") && (s.Substring(s.IndexOf("contain")).Contains(type))).Select(s => s).ToList();
        }

        private static string SubString(string s)
        {
            return !string.IsNullOrEmpty(s) ? s.Substring(0, s.IndexOf("contain") - 1) : "";
        }

        private static void Day7_1_2(string input)
        {
            int result = 0;
            string[] rules = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            const string splitBy = " bags contain ";
            const string bag = "shiny gold";
            Dictionary<string, List<(string bag, int occurence)>> parentRules = new();
            foreach (string rule in rules)
            {
                string[] parts = SplitBy(rule, splitBy).ToArray();
                if (parts[1].StartsWith("no"))
                {
                    parentRules.Add(parts[0], new());
                }
                else
                {
                    var children = SplitBy(parts[1].Replace("bags", "bag").Replace("bag.", ""), " bag, ").ToList();
                    var childInfo = new List<(string bag, int occurence)>();
                    foreach (var child in children)
                    {
                        string[] childParts = child.TrimEnd().Split(' ');
                        if (childParts.Length == 3)
                        {
                            if (int.TryParse(childParts[0], out int occurence))
                            {
                                childInfo.Add(($"{childParts[1]} {childParts[2]}", occurence));
                            }
                            else
                            {
                                throw new ArgumentException($"Could not parse '{childParts[0]}' as a valid number");
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Expected 3 parts, got '{childParts.Length}' as a valid number");
                        }

                    }
                    parentRules.Add(parts[0], childInfo);
                }
            }
            Dictionary<string, List<(string bag, int occurence)>> childRules = new();
            foreach (var key in parentRules.Keys)
            {
                var children = parentRules[key];
                foreach (var child in children)
                {
                    if (!childRules.TryGetValue(child.bag, out var parents))
                    {
                        parents = new();
                        childRules.Add(child.bag, parents);
                    }
                    parents.Add((key, child.occurence));
                }
            }
            HashSet<string> all = new();
            var resultInfo = CountParents((bag, 1), new HashSet<string>(), all, 1, childRules);
            Console.WriteLine($"\r\nHighest: {resultInfo.allSeen.Count-1}, {string.Join(",", resultInfo.allSeen)}");
            result = GetBagCount(bag, parentRules) - 1;
            Console.WriteLine($"\r\nPart2 Result: {result}");
        }

        private static int GetBagCount(string bag, Dictionary<string, List<(string bag, int occurence)>> parentRules)
        {
            int result = 1;

            if(parentRules.TryGetValue(bag, out var children))
            {
                foreach(var child in children)
                {
                    result += child.occurence * GetBagCount(child.bag, parentRules);
                }
            }

            return result;
        }

        private static (int highest, HashSet<string> allSeen) CountParents((string bag, int occurence) bagInfo, HashSet<string> seen, HashSet<string> allSeen, int count, Dictionary<string, List<(string bag, int occurence)>> childRules)
        {
            allSeen.Add(bagInfo.bag);
            int highest = count;
            if (!childRules.TryGetValue(bagInfo.bag, out var children) ||  children.Count == 0)
            {
                //Console.WriteLine($"{count} {string.Join(",", seen)}, {bagInfo.bag}");
            }
            else
            {
                foreach (var child in children)
                {
                    var localSeen = new HashSet<string>(seen.Union(new string[] { bagInfo.bag }));
                    var info = CountParents(child, localSeen, allSeen, count + 1, childRules);
                    allSeen.UnionWith(info.allSeen);
                    if(info.highest > highest)
                    {
                        highest = info.highest;
                    }
                }
            }
            return (highest, allSeen);
        }

        private static IEnumerable<string> SplitBy(string contents, string splitBy)
        {
            var splitLength = splitBy.Length;
            var previousIndex = 0;
            var ix = contents.IndexOf(splitBy);
            while (ix >= 0)
            {
                yield return contents.Substring(previousIndex, ix - previousIndex);
                previousIndex = ix + splitLength;
                ix = contents.IndexOf(splitBy, previousIndex);
            }
            string remain = contents.Substring(previousIndex);
            if (!string.IsNullOrEmpty(remain))
            {
                yield return remain;
            }
        }
        #endregion Day 7

        #region Day 8
        private static void Day8_1(string input)
        {
            string[] fullSet = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var acc = RunInstructions(fullSet, 0);
            Console.WriteLine($"\r\nACC first run: {acc.acc}\r\n");

            var correctedAcc = CorrectError(fullSet);
            Console.WriteLine($"\r\nACC corrected run: {correctedAcc}\r\n");
        }

        private static int CorrectError(string[] fullSet)
        {
            var acc = 0;

            var indexList = Enumerable.Range(0, fullSet.Length-1)
                            .Where(i => fullSet[i].Substring(0, 3).Equals("nop") || fullSet[i].Substring(0, 3).Equals("jmp"))
                            .ToList();
            string[] correctedSet = new string[fullSet.Length];
            foreach (var ixl in indexList)
            {
                fullSet.CopyTo(correctedSet, 0);
                correctedSet[ixl] = fullSet[ixl].Substring(0, 3).Equals("nop") ? correctedSet[ixl].Replace("nop", "jmp") : correctedSet[ixl].Replace("jmp", "nop");
                var tracker = RunInstructions(correctedSet, 0);
                if(tracker.tracker.Contains((fullSet[^1], fullSet.Length - 1)))
                {
                    acc = tracker.acc;
                }
            }
            return acc;
        }

        private static (HashSet<(string set, int index)> tracker, int acc) RunInstructions(string[] fullSet, int acc)
        {
            HashSet<(string set, int index)> tracker = new();
            var ix = 0;
            while (ix < fullSet.Length && !tracker.Contains((fullSet[ix], ix)))
            {
                tracker.Add((fullSet[ix], ix));
                //Console.WriteLine($"Set: {fullSet[ix]} @ {ix}");
                if (Int32.TryParse(fullSet[ix].Substring(4), out int j))
                {
                    switch (fullSet[ix].Substring(0, 3))
                    {
                        case "acc":
                            acc += j;
                            ix++;
                            break;
                        case "jmp":
                            ix = ix + j;
                            break;
                        case "nop":
                            ix++;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("String could not be parsed.");
                }
            }
            foreach (var e in tracker)
            {
                Console.WriteLine($"{e.set} @ {e.index + 1}");
            }
            return (tracker, acc);
        }
        #endregion Day 8

        #region Day 9
        private static void Day9_1(string input, string file)
        {
            Execute(file);
            //var preamble = 25;
            //Queue<long> preambleQ = new();
            //string[] masterList = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //long[] longXmasList = new long[masterList.Length];
            //longXmasList = ParseLongList(masterList);
            //for(var i = 0; i < preamble; i++)
            //{
            //    preambleQ.Enqueue(longXmasList[i]);
            //}
            //(bool valid, long qNumber) test = new();
            //for (var ix = preamble; ix < longXmasList.Length; ix++)
            //{
            //    test = IsValidNumber(preambleQ, longXmasList[ix]);
            //    if (test.valid)
            //    {
            //        preambleQ.Dequeue();
            //        preambleQ.Enqueue(test.qNumber);
            //    } 
            //    else
            //    {
            //        Console.WriteLine($"This number is invalid: {test.qNumber}");
            //        FindWeakness(longXmasList, preambleQ, test.qNumber, ix);
            //        break;
            //    }
            //}
        }

        private static void FindWeakness(long[] list, Queue<long> preambleQ, long target, int lastIndex)
        {
            Queue<long> revPreambleQ = new Queue<long>(preambleQ.Reverse());
            (bool valid, List<long> range) result = new();
            long max = 0, min = 0;
            for (var ix = lastIndex - 26; ix > 0; ix--)
            {
                revPreambleQ.Dequeue();
                revPreambleQ.Enqueue(list[ix]);
                result = FindSumNumbers(revPreambleQ, target);
                if (result.valid)
                {
                    max = result.range.Max();
                    min = result.range.Min();
                    break;
                }
            }
            var sum = max + min;

            Console.WriteLine($"The weakness is: {sum}");
        }

        private static (bool valid, List<long> range) FindSumNumbers(Queue<long> preambleQ, long number)
        {
            bool valid = false;
            List<long> range = new();
            var tempQ = preambleQ.ToArray();

            for (var i = 0; i < tempQ.Length; i++)
            {
                var ix = i;
                var sum = tempQ[i];
                while (ix < tempQ.Length && sum <= number)
                {
                    sum += tempQ[ix];
                    ix++;
                }
                if (sum == number)
                {
                    for(var e = ix; e >= i; e--)
                    {
                        range.Add(tempQ[e]);
                    }
                    valid = true;
                    break;
                }
            }

            return (valid, range);
        }

        private static (bool valid, long qNumber) IsValidNumber(Queue<long> preambleQ, long number)
        {
            (bool valid, long qNumber) result = (false, 0);
            foreach(var n in preambleQ)
            {
                var search = n > number ? n - number : number - n;
                result = (preambleQ.Contains(search), number);
                if (result.valid)
                    break;
            }
            return result;
        }

        private static long[] ParseLongList(string[] list)
        {
            long[] temp = new long[list.Length];
            for (var ix = 0; ix < list.Length; ix++)
            {
                if (long.TryParse(list[ix], out long v))
                {
                    temp[ix] = v;
                }
                else
                {
                    Console.WriteLine($"Could not parse {list[ix]}");
                }
            }
            return temp;
        }

        public static int numberToConsider = 25;
        public static List<long> input;
        public static int firstposition = numberToConsider;
        public static long faultingNumber;

        private static void Execute(string file)
        {
            input = File.ReadAllLines(file).Select(r => long.Parse(r)).ToList();

            //Added a imer just for fun
            var timer = new Stopwatch();
            timer.Start();

            while (true)
            {
                faultingNumber = input[firstposition];
                if (!SumFound(faultingNumber, firstposition))
                    break;
                firstposition++;
            }
            //Part 2:
            var answerPart2 = FindSumUpToAnswer();

            timer.Stop();
            Console.WriteLine($"Part1 answer:  {faultingNumber}");
            Console.WriteLine($"Part2 answer:  {answerPart2}");
            Console.WriteLine($"Executed in: {timer.ElapsedMilliseconds} milliseconds");
        }

        private static long FindSumUpToAnswer()
        {
            firstposition = 0;
            var secondPosition = 1;


            while (true)
            {
                (int start, int end)range = (firstposition, secondPosition -firstposition);
                var listToCheck = input.GetRange(range.start, range.end);
                var rangeResult = listToCheck.Sum();
                if (rangeResult == faultingNumber)
                {
                    return listToCheck.Min() + listToCheck.Max();
                }
                //By adding to either the first or second position a window is created and results are re-used instead of creating a new list every time
                if (rangeResult < faultingNumber)
                    secondPosition++;
                else
                    firstposition++;
            }
        }

        private static bool SumFound(long nextNumberToCheck, int position)
        {
            var workingList = input.Skip(position - numberToConsider).Take(numberToConsider).ToList();
            workingList.Sort();
            for (int i = 0; i < numberToConsider - 1; i++)
            {
                var valueNeeded = nextNumberToCheck - workingList[i];
                var result = workingList.BinarySearch(valueNeeded);
                if (result >= 0)
                    return true;
            }
            return false;
        }

        #endregion Day 9

        #region Day 10

        private static void Day10(string input)
        {
            List<int> adaptors = File.ReadAllLines(input).Select(r => int.Parse(r)).ToList();
            adaptors.Sort();
            var max = adaptors.Max();
            var device = max + 3;
            int oneJolt = 0;
            int threeJolt = 1;
            oneJolt = adaptors.Min() - 0 == 1 ? 1 : adaptors.Min() - 0 == 3 ? 0 : threeJolt++;
            for (var i = 0; i < adaptors.Count - 1; i++)
            {
                if(adaptors[i+1]-adaptors[i] == 1)
                {
                    oneJolt++;
                }
                
                if(adaptors[i + 1] - adaptors[i] == 3)
                {
                    threeJolt++;
                }
            }
            Console.WriteLine($"Sums:\r\n1J: {oneJolt} 3J: {threeJolt}");
            Console.WriteLine($"Result: {oneJolt*threeJolt}\r\n");

            Day10_2(adaptors, max);
        }

        private static void Day10_2(List<int> list, int max)
        {
            list.Add(0);
            list.Add(max);
            list.Sort();
            //Get list of length of all continguous 1-jolt adapters gaps.
            //The adapters on the ends are not counted as they are required to be in the sequence to support their 3-jolt gap.
            //Eg: 0    3 4 5 6 7 8    11 -> count is 5 (4-8)
            //    0    3 4 5     8       -> count is 1 (4)
            //    0    3 4 5 6      9    -> count is 2 (4-6)

            List<int> oneJoltRunLengths = new List<int>();
            int contiguousCount = 0;
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i + 1] - list[i] == 1)
                {
                    contiguousCount++;
                }
                else
                {
                    contiguousCount--;
                    if (contiguousCount >= 1)
                    {
                        oneJoltRunLengths.Add(contiguousCount);
                    }
                    contiguousCount = 0;
                }

            }

            //Getting the total number of combinations means finding how many ways in which adapters can be left unused from sequence
            //Adapters can be left out as long as they don't create a three-jolt gap.
            //The list of oneJoltRunLengths has been prepared as counts of
            //  runs of adapters that can be removed in that they're not required to span a 3-jolt gap (see above). So the question is, how many
            //  combinations are there for a run of 1-jolt adapter gaps that don't create a gap of 3 or more.
            //For a single adapter, there are two ways.  (0, 1)
            //For two adapters in a row, there are four. (00, 01, 10, 11)
            //For three, there are seven ways            (001, 010, 011, 100, 101, 110, 111 -> just not 000)
            //Past that point, this approach can be used:
            //    https://math.stackexchange.com/questions/2844818/coin-tossing-problem-where-three-tails-come-in-a-row
            //
            //However, the data set as provided only had gaps of 1, 2, and 3 jolts, so the solutions for those gaps are fixed in the
            //  array runCombinations rather than a generalized solution.
            //To get the total combination, multiply the number of combinations of each run by each other!
            long totalCombinations = 1;
            int[] runCombinations = { 1, 2, 4, 7 };
            foreach (int c in oneJoltRunLengths)
            {
                totalCombinations *= runCombinations[c];
            }

            Console.WriteLine($"Combinations: {totalCombinations}");
        }

        #endregion Day 10

        #region Day 11
        private static void Day11(string input)
        {
            /*  
                L.LL.LL.LL
                LLLLLLL.LL
                L.L.L..L..
                LLLL.LL.LL
                L.LL.LL.LL
                L.LLLLL.LL
                ..L.L.....
                LLLLLLLLLL
                L.LLLLLL.L
                L.LLLLL.LL
                
                All decisions are based on the number of occupied seats adjacent to a given seat 
                (one of the eight positions immediately up, down, left, right, or diagonal from the seat).
                The following rules are applied to every seat simultaneously:
                   - If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                   - If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                   - Otherwise, the seat's state does not change.

                Floor (.) never changes; seats don't move, and nobody sits on the floor.
            */
            string[] seats = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<List<Seat>> seatMap = new();
            CreateSeatMap(seats, seatMap);
            Console.WriteLine($"{seatMap[0][0]}");
            bool changed = true;
            while (changed)
            {
                Console.WriteLine("Running Rules and Checking for Changed.");
                foreach (var row in seatMap)
                {
                    foreach (var seat in row)
                    {
                        //rules for change - trans = change
                        if(!seat.CurrentState.Equals(States.Floor))
                        {
                            int emptyCount = 0;
                            foreach (var adjSeat in seat.AdjacentSeats)
                            {
                                if (seatMap[adjSeat.row][adjSeat.col].CurrentState.Equals(States.Empty))
                                    emptyCount++;
                            }
                            if (seat.CurrentState.Equals(States.Empty))
                            {
                                if (emptyCount == seat.AdjacentSeats.Count)
                                {
                                    seat.TransitionState = States.Occupied;
                                }
                            } 
                            else
                            {
                                if (seat.AdjacentSeats.Count - emptyCount >= 5)
                                {
                                    seat.TransitionState = States.Empty;
                                }
                            }
                        }
                    }
                }
                foreach (var row in seatMap)
                {
                    foreach (var seat in row)
                    {
                        // prev = cur
                        // cur = trans
                        if (!seat.CurrentState.Equals(States.Floor))
                        {
                            seat.PreviousState = seat.CurrentState;
                            if (!seat.TransitionState.Equals(seat.CurrentState))
                            {
                                seat.CurrentState = seat.TransitionState;
                            }
                            seat.IsChanged();
                        }
                    }
                }
                int chgCount = 0;
                foreach (var row in seatMap)
                {
                    chgCount += row.Any(x => x.Changed) ? 1 : 0;
                }
                changed = chgCount > 0;
            }

            int occupied = 0;
            foreach (var row in seatMap)
            {
                occupied += row.Where(s => s.CurrentState.Equals(States.Occupied)).Select(s => s).Count();
            }
            
            Console.WriteLine($"\r\nOccupied Seats: {occupied}");
        }

        private static void CreateSeatMap(string[] seats, List<List<Seat>> seatMap)
        {
            for (int ir = 0; ir < seats.Length; ir++)
            {
                var ic = 0;
                List<Seat> tList = new();
                while (ic < seats[0].Length)
                {
                    var seat = seats[ir].ElementAt(ic);
                    var temp = new Seat();
                    switch (seat)
                    {
                        case 'L':
                            temp.CurrentState = States.Empty;
                            temp.PreviousState = temp.CurrentState;
                            temp.TransitionState = temp.PreviousState;
                            break;
                        case '.':
                            temp.CurrentState = States.Floor;
                            temp.PreviousState = temp.CurrentState;
                            temp.TransitionState = temp.PreviousState;
                            break;
                        default:
                            break;
                    }
                    tList.Add(temp);
                    ic++;
                }
                seatMap.Add(tList);
            }

            for (int ir = 0; ir < seatMap.Count; ir++)
            {
                var ic = 0;
                while (ic < seatMap[0].Count)
                {
                    if (!seatMap[ir][ic].CurrentState.Equals(States.Floor))
                    {
                        seatMap[ir][ic].AdjacentSeats = CountSeats((ir, ic), seatMap);
                    }
                    ic++;
                }
            }
        }

        private static List<(int row, int col)> CountSeats((int row, int col) position, List<List<Seat>> seatMap)
        {
            List<(int row, int col)> temp = new();
            //Up
            if (position.row > 0)
            {
                //Up Left
                if (position.col > 0)
                {
                    if (!seatMap[position.row - 1][position.col - 1].CurrentState.Equals(States.Floor))
                    {
                        temp.Add((position.row - 1, position.col - 1));
                    }
                    else
                    {
                        for (int ir = position.row - 2, ic = position.col - 2; ir >= 0 && ic >= 0; ir--, ic--)
                        {
                            if (!seatMap[ir][ic].CurrentState.Equals(States.Floor))
                            {
                                temp.Add((ir, ic));
                                ir = ic = -1;
                            }
                        }
                    }
                }
                    
                //Up
                if (!seatMap[position.row - 1][position.col].CurrentState.Equals(States.Floor))
                {
                    temp.Add((position.row - 1, position.col));
                }
                else
                {
                    for (int ir = position.row - 2; ir >= 0; ir--)
                    {
                        if (!seatMap[ir][position.col].CurrentState.Equals(States.Floor))
                        {
                            temp.Add((ir, position.col));
                            ir = -1;
                        }
                    }
                }

                //Up Right
                if (position.col < seatMap[position.row].Count - 1)
                {
                    if (!seatMap[position.row - 1][position.col + 1].CurrentState.Equals(States.Floor))
                    {
                        temp.Add((position.row - 1, position.col + 1));
                    }
                    else
                    {
                        for (int ir = position.row - 2, ic = position.col + 2; ir >= 0 && ic < seatMap[position.row].Count; ir--, ic++)
                        {
                            if (!seatMap[ir][ic].CurrentState.Equals(States.Floor))
                            {
                                temp.Add((ir, ic));
                                ir = -1; 
                                ic = seatMap.Count + seatMap[position.row].Count;
                            }
                        }
                    }
                }
                    

            }

            //Same Row - Left
            if (position.col > 0)
            {
                if (!seatMap[position.row][position.col - 1].CurrentState.Equals(States.Floor))
                {
                    temp.Add((position.row, position.col - 1));
                }
                else
                {
                    for (int ic = position.col - 2; ic >= 0; ic--)
                    {
                        if (!seatMap[position.row][ic].CurrentState.Equals(States.Floor))
                        {
                            temp.Add((position.row, ic));
                            ic = -1;
                        }
                    }
                }
            }

            //Same Row - Right
            if (position.col < seatMap[position.row].Count - 1)
            {
                if (!seatMap[position.row][position.col + 1].CurrentState.Equals(States.Floor))
                {
                    temp.Add((position.row, position.col + 1));
                }
                else
                {
                    for (int ic = position.col + 2; ic < seatMap[position.row].Count; ic++)
                    {
                        if (!seatMap[position.row][ic].CurrentState.Equals(States.Floor))
                        {
                            temp.Add((position.row, ic));
                            ic = seatMap.Count + seatMap[position.row].Count;
                        }
                    }
                }
            }
                
            //Down
            if (position.row < seatMap.Count - 1)
            {
                //Down Left
                if (position.col > 0)
                {
                    if (!seatMap[position.row + 1][position.col - 1].CurrentState.Equals(States.Floor))
                    {
                        temp.Add((position.row + 1, position.col - 1));
                    }
                    else
                    {
                        for (int ir = position.row + 2, ic = position.col - 2; ir < seatMap.Count && ic >= 0; ir++, ic--)
                        {
                            if (!seatMap[ir][ic].CurrentState.Equals(States.Floor))
                            {
                                temp.Add((ir, ic));
                                ir = seatMap.Count + seatMap[position.row].Count; 
                                ic = -1;
                            }
                        }
                    }
                }
                    
                //Down
                if(!seatMap[position.row + 1][position.col].CurrentState.Equals(States.Floor))
                {
                    temp.Add((position.row + 1, position.col));
                }
                else
                {
                    for (int ir = position.row + 2; ir < seatMap.Count; ir++)
                    {
                        if (!seatMap[ir][position.col].CurrentState.Equals(States.Floor))
                        {
                            temp.Add((ir, position.col));
                            ir = seatMap.Count + seatMap[position.row].Count;
                        }
                    }
                }

                //Down Right
                if (position.col < seatMap[position.row].Count - 1)
                {
                    if (!seatMap[position.row + 1][position.col + 1].CurrentState.Equals(States.Floor))
                    {
                        temp.Add((position.row + 1, position.col + 1));
                    }
                    else
                    {
                        for (int ir = position.row + 2, ic = position.col + 2; ir < seatMap.Count && ic < seatMap[ir].Count; ir++, ic++)
                        {
                            if (!seatMap[ir][ic].CurrentState.Equals(States.Floor))
                            {
                                temp.Add((ir, ic));
                                ir = seatMap.Count + seatMap[position.row].Count;
                                ic = seatMap.Count + ir;
                            }
                        }
                    }
                }
                    
            }
            return temp;
        }

        #endregion Day 11

        #region Day 12

        private static void Day12(string[] input)
        {
            List<(string direction, int value)> instructions = new();
            (int x, int y) compass = (0, 0);
            (int x, int y) waypoint = (10, 1);
            foreach (var e in input)
            {
                var direction = e.Substring(0,1);
                var value = int.Parse(e.Substring(1));
                instructions.Add((direction, value));
            }
            
            foreach (var ins in instructions)
            {
                Console.WriteLine($"Input: {ins.direction}{ins.value}");
                string dir = ins.direction.ToLower();
                if (dir.Equals("f"))
                {
                    compass.x += waypoint.x * ins.value;
                    compass.y += waypoint.y * ins.value;
                    Console.WriteLine($"Compass: ({compass.x}, {compass.y})");
                }
                switch (dir)
                {
                    case "n":
                        waypoint.y += ins.value;
                        break;
                    case "e":
                        waypoint.x += ins.value;
                        break;
                    case "s":
                        waypoint.y -= ins.value;
                        break;
                    case "w":
                        waypoint.x -= ins.value;
                        break;
                    case "l":
                    case "r":
                        waypoint = SwitchDirection(waypoint, dir, ins.value);
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"Waypoint: ({waypoint.x}, {waypoint.y})");
            }

            Console.WriteLine($"Manhattan distance: {Math.Abs(compass.x) + Math.Abs(compass.y)}");
        }

        private static (int x, int y) SwitchDirection((int x, int y) waypoint, string turnDir, int degree)
        {
            (int x, int y) dir = waypoint;
            (int x, int y) point = new();
            point = RotateCoords(waypoint, turnDir, degree);
            dir.x = point.x;
            dir.y = point.y;
            return dir;
        }

        private static (int x, int y) RotateCoords((int x, int y) point, string turn, int degree)
        {
            int newX, newY;
            if (turn.Equals("l") && degree == 90 || turn.Equals("r") && degree == 270)
            {
                newX = point.y *-1;
                newY = point.x;
            }
            else if (turn.Equals("l") && degree == 270 || turn.Equals("r") && degree == 90)
            {
                newX = point.y;
                newY = point.x * -1;
            }
            else
            {
                newX = point.x * -1;
                newY = point.y * -1;
            }
            return (newX, newY);
        }

        #endregion Day 12

        #region Day 13
        private static void Day13(string[] input)
        {
            var departTime = int.Parse(input[0]);
            List<int> buses = new();
            List<(int bus, int time, int diff, int result)> schedules = new();
            var temp = input[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var e in temp)
            {
                if (!e.ToLower().Equals("x"))
                {
                    buses.Add(int.Parse(e));
                }
                else
                {
                    buses.Add(0);
                }
            }
            foreach (var bus in buses)
            {
                if(bus > 0)
                {
                    int i = 0;
                    while ((departTime + i) % bus != 0)
                    {
                        i++;
                    }
                    schedules.Add((bus, departTime + i, i, (bus * i)));
                }
                else
                {
                    schedules.Add((bus, bus, 999999999, 0));
                }
            }
            var sortedSchedules = schedules.OrderBy(o => o.diff).ToList();
            Console.WriteLine($"First Result: {sortedSchedules.First().result}\r\n");
            Day13_2(buses);
        }

        private static void Day13_2(List<int> buses)
        {
            List<Schedule> schedules = new();
            bool found = false;
            long time = 100000000000000;
            foreach (var b in buses)
            {
                var temp = new Schedule()
                {
                    Bus = b,
                    Time = time,
                    Result = b != 0 ? ModIsZero(time, b) : false
                };
                schedules.Add(temp);
            }
            while (!found)
            {
                foreach (var s in schedules)
                {
                    if (s.Bus != 0)
                    {
                        s.Time = time;
                        var index = schedules.IndexOf(s);
                        s.Result = ModIsZero(s.Time + index, s.Bus);
                    }
                }
                found = schedules.TrueForAll(s => s.Result);
                if (schedules.First().Result && schedules[schedules.Count-1].Result)
                {
                    var product = schedules.Where(b => b.Result).Select(b => b.Bus).ToList();
                    var p = 1;
                    foreach (var b in product)
                    {
                        p = p * b;
                    }
                    time += p;
                }
                else if (schedules.First().Result)
                {
                    time += schedules.First().Bus;
                } 
                else
                {
                    time++;
                }
                
                Console.Write($"\rTime: {time}                               {DateTime.Now}                            ");
            }

            Console.WriteLine($"Second Result: {time - buses.Count}");
        }

        private static bool ModIsZero(long number, int divisor)
        {
            return number % divisor == 0;
        }

        #endregion Day 13

        #region Day 14


        #endregion Day 14

        #region Day 15


        #endregion Day 15

        #region Day 16


        #endregion Day 16

        #region Day 17


        #endregion Day 17

        #region Day 18


        #endregion Day 18

        #region Day 19


        #endregion Day 19

        #region Day 20


        #endregion Day 20

        #region Day 21


        #endregion Day 21

        #region Day 22


        #endregion Day 22

        #region Day 23


        #endregion Day 23

        #region Day 24


        #endregion Day 24

        #region Day 25


        #endregion Day 25



    }
}

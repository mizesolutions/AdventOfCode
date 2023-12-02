namespace AdventOfCode2023.Days
{
    public class Day01 : BaseDay
    {
        public string First { get; set; }
        public string Second { get; set; }
        public List<string> CalibrationValues { get; set; }
        public Dictionary<string, string> Digits { get; set; }
        public List<string> LetterList { get; set; }

        public Day01(string day, bool hasInput) : base(day, hasInput)
        {
            First = "";
            Second = "";
            CalibrationValues = new List<string>();
            Digits = new Dictionary<string, string>()
            {
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6"  },
                { "seven", "7" },
                { "eight", "8"  },
                { "nine", "9" },
                { "zero", "0" }
            };
            LetterList = new List<string>()
            {
                "o", "t", "f", "s", "e", "n", "z"
            };
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            CalibrationValues = new List<string>();
            Result1 = 0;
            Result2 = 0;

            foreach (var s in FileInput)
            {
                First = "";
                Second = "";

                foreach (var c in s)
                {
                    if (Char.IsDigit(c))
                    {
                        var num = c.ToString();
                        if (string.IsNullOrEmpty(First))
                        {
                            First = num;
                        }
                        else
                        {
                            Second = num;
                        }
                    }
                }

                if (string.IsNullOrEmpty(Second))
                {
                    CalibrationValues.Add(string.Concat(new[] { First, First }));
                }
                else
                {
                    CalibrationValues.Add(string.Concat(new[] { First, Second }));
                }
            }
            foreach (var num in CalibrationValues)
            {
                if (Int64.TryParse(num, out var result))
                    Result1 += result;
            }
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            CalibrationValues = new List<string>();
            Result1 = 0;
            Result2 = 0;

            foreach (var s in FileInput)
            {
                First = "";
                Second = "";

                var tempString = "";

                for (var i = 0; i < s.Length; i++)
                {
                    if (Char.IsDigit(s[i]) && string.IsNullOrEmpty(First))
                    {
                        First = s[i].ToString();
                        continue;
                    }
                    else if (Char.IsDigit(s[i]))
                    {
                        Second = s[i].ToString();
                        continue;
                    }
                    else
                    {
                        //   9ninehbsgkcthree1nineeightsix9
                        if (LetterList.Contains(s[i].ToString()) || (tempString.Length > 0 && LetterList.Contains(tempString[0].ToString())))
                        {
                            switch (s[i].ToString())
                            {
                                case "o":
                                    if (s.Substring(i).Length > 2 && s[i + 1].ToString().Equals("n") && s[i + 2].ToString().Equals("e"))
                                    {
                                        tempString = "1";
                                    }
                                    break;
                                case "t":
                                    if (s.Substring(i).Length > 2 && s[i + 1].ToString().Equals("w") && s[i + 2].ToString().Equals("o"))
                                    {
                                        tempString = "2";
                                    }
                                    else if (s.Substring(i).Length > 4 && s[i + 1].ToString().Equals("h") && s[i + 2].ToString().Equals("r") && s[i + 3].ToString().Equals("e") && s[i + 4].ToString().Equals("e"))
                                    {
                                        tempString = "3";
                                    }
                                    break;
                                case "f":
                                    if (s.Substring(i).Length > 3 && s[i + 1].ToString().Equals("o") && s[i + 2].ToString().Equals("u") && s[i + 3].ToString().Equals("r"))
                                    {
                                        tempString = "4";
                                    }
                                    else if (s.Substring(i).Length > 3 && s[i + 1].ToString().Equals("i") && s[i + 2].ToString().Equals("v") && s[i + 3].ToString().Equals("e"))
                                    {
                                        tempString = "5";
                                    }
                                    break;
                                case "s":
                                    if (s.Substring(i).Length > 2 && s[i + 1].ToString().Equals("i") && s[i + 2].ToString().Equals("x"))
                                    {
                                        tempString = "6";
                                    }
                                    else if (s.Substring(i).Length > 4 && s[i + 1].ToString().Equals("e") && s[i + 2].ToString().Equals("v") && s[i + 3].ToString().Equals("e") && s[i + 4].ToString().Equals("n"))
                                    {
                                        tempString = "7";
                                    }
                                    break;
                                case "e":
                                    if (s.Substring(i).Length > 4 && s[i + 1].ToString().Equals("i") && s[i + 2].ToString().Equals("g") && s[i + 3].ToString().Equals("h") && s[i + 4].ToString().Equals("t"))
                                    {
                                        tempString = "8";
                                    }
                                    break;
                                case "n":
                                    if (s.Substring(i).Length > 3 && s[i + 1].ToString().Equals("i") && s[i + 2].ToString().Equals("n") && s[i + 3].ToString().Equals("e"))
                                    {
                                        tempString = "9";
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }

                        if (!string.IsNullOrEmpty(tempString))
                        {
                            if (string.IsNullOrEmpty(First))
                            {
                                First = tempString;
                            }
                            else
                            {
                                Second = tempString;
                            }
                            tempString = "";
                        }
                    }
                }

                if (string.IsNullOrEmpty(Second))
                {
                    CalibrationValues.Add(string.Concat(new[] { First, First }));
                }
                else
                {
                    CalibrationValues.Add(string.Concat(new[] { First, Second }));
                }

            }

            foreach (var num in CalibrationValues)
            {
                if (Int64.TryParse(num, out var result))
                    Result2 += result;
            }

            PrintResults(Result2);
        }
    }
}
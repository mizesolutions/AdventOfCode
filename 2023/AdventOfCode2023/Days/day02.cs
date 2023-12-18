namespace AdventOfCode2023.Days
{
    public class Day02 : BaseDay
    {
        public Dictionary<string, string> BagContents { get; set; }
        public bool IsImpossible { get; set; }
        public HashSet<int> PossibleGameIds { get; set; }
        public int Index { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public List<long> PowersList { get; set; }

        public Day02(string day, bool hasInput) : base(day, hasInput)
        {
            BagContents = new Dictionary<string, string>();
            IsImpossible = false;
            PossibleGameIds = new HashSet<int>();
            Index = Red = Green = Blue = 0;
            PowersList = new List<long>();
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            Result1 = 0;

            BagContents = new Dictionary<string, string>()
            {
                {"red", "12"},
                {"green", "13"},
                {"blue", "14"}
            };

            foreach (var r in FileInput)
            {
                IsImpossible = false;
                for (Index = 8; Index < r.Length - 1; Index++)
                {
                    if (!Char.IsDigit(r[Index]))
                    {
                        continue;
                    }

                    if (Char.IsDigit(r[Index]) && Char.IsDigit(r[Index + 1]))
                    {
                        var countString = string.Concat(new[] { r[Index], r[Index + 1] });
                        int.TryParse(countString, out var gameNum);

                        if (Char.IsLetter(r[Index + 3]))
                        {
                            switch (r[Index + 3])
                            {
                                case 'r':
                                    CheckValue(r, "red", gameNum);
                                    break;
                                case 'g':
                                    CheckValue(r, "green", gameNum);
                                    break;
                                case 'b':
                                    CheckValue(r, "blue", gameNum);
                                    break;
                                default:
                                    break;
                            }
                        }
                        Index = IsImpossible ? 400 : Index;
                    }
                    if (!IsImpossible)
                    {
                        AddOrRemoveCheckedValue(r, IsImpossible);
                    }
                    IsImpossible = false;
                }
            }
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            Result2 = 0;
            Index = 0;

            foreach (var r in FileInput)
            {
                for (Index = 8; Index < r.Length - 1; Index++)
                {
                    var temp = 0;
                    if (!Char.IsDigit(r[Index]) && (!r[Index].Equals('r') || !r[Index].Equals('g') || !r[Index].Equals('b')))
                    {
                        continue;
                    }
                    else
                    {
                        if (Char.IsDigit(r[Index]) && Char.IsDigit(r[Index + 1]))
                        {
                            var numString = string.Concat(new[] { r[Index], r[Index + 1] });
                            _ = int.TryParse(numString, out temp);
                        }
                        else
                        {
                            _ = int.TryParse(r[Index].ToString(), out temp);
                        }

                        while (!Char.IsLetter(r[Index]))
                        {
                            Index++;
                        }

                        switch (r[Index])
                        {
                            case 'r':
                                Red = Red < temp ? temp : Red;
                                break;
                            case 'g':
                                Green = Green < temp ? temp : Green;
                                break;
                            case 'b':
                                Blue = Blue < temp ? temp : Blue;
                                break;
                            default:
                                break;
                        }
                    }


                }
                PowersList.Add(Red * Green * Blue);
                Red = Green = Blue = 0;
            }

            foreach (var p in PowersList)
            {
                Result2 += p;
            }

            PrintResults(Result2);
        }


        private void PrintList<T>(List<T> list)
        {
            foreach (var s in list)
            {
                Console.Write(s);
            }
            Console.WriteLine("");
        }

        private void CheckValue(string r, string key, int gameNum)
        {
            BagContents.TryGetValue(key, out var val);
            int.TryParse(val, out var cubeNum);
            IsImpossible = gameNum > cubeNum;
            AddOrRemoveCheckedValue(r, IsImpossible);
        }

        private void AddOrRemoveCheckedValue(string r, bool remove)
        {
            var gameId = "";

            if (Char.IsDigit(r[5]) && Char.IsDigit(r[6]) && Char.IsDigit(r[7]))
            {
                gameId = string.Concat(new[] { r[5], r[6], r[7] });
            }
            else if (Char.IsDigit(r[5]) && Char.IsDigit(r[6]))
            {
                gameId = string.Concat(new[] { r[5], r[6] });
            }
            else
            {
                gameId = r[5].ToString();
            }

            int.TryParse(gameId, out var intGameId);

            if (!remove && PossibleGameIds.Add(intGameId))
            {
                Result1 += intGameId;
            }
            else if (remove && PossibleGameIds.Remove(intGameId))
            {
                if (Result1 > 0)
                    Result1 -= intGameId;
            }
        }
    }
}
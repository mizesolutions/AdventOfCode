namespace AdventOfCode2023.Days
{
    public class Day03 : BaseDay
    {
        public List<string> NumberStrings { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Index { get; set; }

        public Day03(string day, bool hasInput) : base(day, hasInput)
        {
            NumberStrings = new List<string>();
            Top = Right = Bottom = Left = Index = 0;
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();

            Right = FileInput[0].Length - 1;
            Bottom = FileInput.Count - 1;

            for (int i = 0; i < FileInput.Count - 1; i++)
            {
                for (Index = 0; Index < FileInput[i].Length - 1; Index++)
                {
                    if (FileInput[i][Index].Equals('.'))
                    {
                        continue;
                    }
                    else if (Char.IsDigit(FileInput[i][Index]))
                    {
                        CheckForSymbols(FileInput[i][Index], i);
                    }
                    else
                    {
                        continue;
                    }
                }

            }

            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }

        private void CheckForSymbols(char number, int row)
        {
            var i = Index;
            NumberStrings = new List<string>();
            var count = 0;
            //row up
            if (row - 1 >= Top)
            {
                //check left
                if (Index >= Left)
                {
                    if (!Char.IsDigit(FileInput[row - 1][i]) && !FileInput[row - 1][i].Equals('.'))
                    {
                        NumberStrings.Add(FileInput[row][i].ToString());
                        if (i - 1 >= Left && Char.IsDigit(FileInput[row][i - 1]))
                        {
                            NumberStrings.Add(FileInput[row][i - 1].ToString());
                            if (i - 2 >= Left && Char.IsDigit(FileInput[row][i - 2]))
                            {
                                NumberStrings.Add(FileInput[row][i - 2].ToString());
                            }
                        }
                    }
                    else if (i - 1 >= Left && !Char.IsDigit(FileInput[row - 1][i - 1]) && !FileInput[row - 1][i - 1].Equals('.'))
                    {
                        NumberStrings.Add(FileInput[row][i].ToString());
                        if (i - 1 >= Left && Char.IsDigit(FileInput[row][i - 1]))
                        {
                            NumberStrings.Add(FileInput[row][i - 1].ToString());
                            if (i - 2 >= Left && Char.IsDigit(FileInput[row][i - 2]))
                            {
                                NumberStrings.Add(FileInput[row][i - 2].ToString());
                            }
                        }
                    }
                    else if (i + 1 <= Right && !Char.IsDigit(FileInput[row - 1][i + 1]) && !FileInput[row - 1][i + 1].Equals('.'))
                    {
                        NumberStrings.Add(FileInput[row][i].ToString());
                        if (i + 1 <= Right && Char.IsDigit(FileInput[row][i + 1]))
                        {
                            NumberStrings.Add(FileInput[row][i + 1].ToString());
                            if (i + 2 <= Right && Char.IsDigit(FileInput[row][i + 2]))
                            {
                                NumberStrings.Add(FileInput[row][i + 2].ToString());
                            }
                        }
                    }
                }
            }

            //row down
            if (row + 1 <= Bottom)
            {
                //check left
                if (Index >= Left)
                {
                    if (!Char.IsDigit(FileInput[row + 1][i]) && !FileInput[row + 1][i].Equals('.'))
                    {
                        NumberStrings.Add(FileInput[row][i].ToString());
                        if (i - 1 >= Left && Char.IsDigit(FileInput[row][i - 1]))
                        {
                            NumberStrings.Add(FileInput[row][i - 1].ToString());
                            if (i - 2 >= Left && Char.IsDigit(FileInput[row][i - 2]))
                            {
                                NumberStrings.Add(FileInput[row][i - 2].ToString());
                            }
                        }
                    }
                    else if (i - 1 >= Left && !Char.IsDigit(FileInput[row + 1][i - 1]) && !FileInput[row + 1][i - 1].Equals('.'))
                    {
                        NumberStrings.Add(FileInput[row][i].ToString());
                        if (i - 1 >= Left && Char.IsDigit(FileInput[row][i - 1]))
                        {
                            NumberStrings.Add(FileInput[row][i - 1].ToString());
                            if (i - 2 >= Left && Char.IsDigit(FileInput[row][i - 2]))
                            {
                                NumberStrings.Add(FileInput[row][i - 2].ToString());
                            }
                        }
                    }
                    else if (i + 1 <= Right && !Char.IsDigit(FileInput[row + 1][i + 1]) && !FileInput[row + 1][i + 1].Equals('.'))
                    {
                        NumberStrings.Add(FileInput[row][i].ToString());
                        if (i + 1 <= Right && Char.IsDigit(FileInput[row][i + 1]))
                        {
                            NumberStrings.Add(FileInput[row][i + 1].ToString());
                            if (i + 2 <= Right && Char.IsDigit(FileInput[row][i + 2]))
                            {
                                NumberStrings.Add(FileInput[row][i + 2].ToString());
                            }
                        }
                        else if (i - 1 >= Left && Char.IsDigit(FileInput[row][i - 1]))
                        {
                            NumberStrings.Add(FileInput[row][i - 1].ToString());
                            if (i - 2 >= Left && Char.IsDigit(FileInput[row][i - 2]))
                            {
                                NumberStrings.Add(FileInput[row][i - 2].ToString());
                            }
                        }
                    }
                }
                NumberStrings.Reverse();
                var numString = string.Join("", [.. NumberStrings]);
                int.TryParse(numString, out var num);
                Result1 += num;
            }

            //index left

            //index right

            //row down

        }
    }
}
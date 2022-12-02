using System.Collections.Generic;

namespace AdventOfCode2021.Days
{
    class Day04 : BaseDay
    {
        public Day04(string day, bool hasInput) : base(day, hasInput)
        {
            Input.MixedDataRender();
        }

        public void PuzzleOne()
        {
            PrintCurrentMethod();
            Result1 = 0;
            Result2 = 0;
            bool end = false;
            List<int[,]> winOrder = new();
            foreach (var number in Input.IntList)
            {
                foreach (var m in Input.IntMatrixList)
                {
                    end = false;
                    if (m[0,0] == 0)
                    {
                        for (int row = 1, col = 1; row < m.GetLength(1); row++, col = 1)
                        {
                            for (; col < m.GetLength(0); col++)
                            {
                                if (m[row, col] == number)
                                {
                                    m[row, 0]++;
                                    m[0, col]++;
                                    m[row, col] = -1;
                                }
                                if (CheckBoard(m, row, col))
                                {
                                    m[0, 0] = number;
                                    winOrder.Add(m);
                                    end = true;
                                }
                                if (end)
                                    break;
                            }
                            if (end)
                                break;
                        }
                    }
                }
            }
            Result1 = SumBoard(winOrder[0]);
            Result2 = SumBoard(winOrder[^1]);
            PrintResults(Result1);
        }

        public void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }

        private bool CheckBoard(int[,] matrix, int row, int col)
        {
            return matrix[row, 0] == 5 || matrix[0, col] == 5;
        }

        private int SumBoard(int[,] m)
        {
            int number = m[0, 0];
            int sum = 0;
            for(int row = 1, col = 1; row < m.GetLength(1); row++, col = 1)
            {
                for(; col < m.GetLength(0); col++)
                {
                    if (m[row, col] > -1)
                        sum += m[row, col];
                }
            }
            return sum * number;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day08 : BaseDay
    {
        public int[,] Trees { get; set; }

        public Day08 (string day, bool hasInput) : base(day, hasInput)
        {
            Trees = SetTrees();
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            Result1 = WhatYouSee();
            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            Result2 = BestView();
            PrintResults(Result2);
        }

        private int[,] SetTrees()
        {
            int row = FileInput[0].Length;
            int col = FileInput.Count;

            int[,] trees = new int[row, col];
            for (int i = 0; i < FileInput.Count; i++)
            {
                var temp = FileInput[i].ToArray();
                for(var j =0; j < temp.Length; j++)
                {
                    trees[i, j] = int.Parse(temp[j].ToString());
                }
            }
            return trees;
        }

        private int WhatYouSee()
        {
            var thisMany = 0;
            for(int r = 0; r < Trees.GetLength(0); r++)
            {
                for (var c = 0; c < Trees.GetLength(1); c++)
                {
                    thisMany += IsVisible(c, r) ? 1 : 0;
                }
            }
            return thisMany;
        }

        private bool IsVisible(int col, int row)
        {
            int tree = Trees[row, col];
            return CheckUp(col, row, tree) || CheckDown(col, row, tree) || CheckLeft(col, row, tree) || CheckRight(col, row, tree);
        }

        private bool CheckUp(int c, int r, int tree)
        {
            for (int i = r - 1; i >= 0; i--)
            {
                if (Trees[i, c] >= tree)
                {
                    return false;
                }
            }
            return true;
        }

       private bool CheckDown(int c, int r, int tree)
        {
            for (int i = r + 1; i < Trees.GetLength(0); i++)
            {
                if (Trees[i, c] >= tree)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckLeft(int c, int r, int tree)
        {
            for (int i = c - 1; i >= 0; i--)
            {
                if (Trees[r, i] >= tree)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckRight(int c, int r, int tree)
        {
            for (int i = c + 1; i < Trees.GetLength(1); i++)
            {
                if (Trees[r, i] >= tree)
                {
                    return false;
                }
            }
            return true;
        }

        private int BestView()
        {
            int bestView = 0;
            for (int r = 0; r < Trees.GetLength(0); r++)
            {
                for (var c = 0; c < Trees.GetLength(1); c++)
                {
                    int treeScore = ScenicScore(c, r);
                    bestView = treeScore > bestView ? treeScore : bestView;
                }
            }
            return bestView;
        }
        private int ScenicScore(int col, int row)
        {
            int tree = Trees[row, col];
            return CheckUpCount(col, row, tree) * CheckDownCount(col, row, tree) * CheckLeftCount(col, row, tree) * CheckRightCount(col, row, tree);
        }

        private int CheckUpCount(int c, int r, int tree)
        {
            int count = 0;
            for (int i = r - 1; i >= 0; i--)
            {
                if (Trees[i, c] >= tree)
                {
                    return count + 1;
                }
                count++;
            }
            return count;
        }

        private int CheckDownCount(int c, int r, int tree)
        {
            int count = 0;
            for (int i = r + 1; i < Trees.GetLength(0); i++)
            {
                if (Trees[i, c] >= tree)
                {
                    return count + 1;
                }
                count++;
            }
            return count;
        }

        private int CheckLeftCount(int c, int r, int tree)
        {
            int count = 0;
            for (int i = c - 1; i >= 0; i--)
            {
                if (Trees[r, i] >= tree)
                {
                    return count + 1;
                }
                count++;
            }
            return count;
        }

        private int CheckRightCount(int c, int r, int tree)
        {
            int count = 0;
            for (int i = c + 1; i < Trees.GetLength(1); i++)
            {
                if (Trees[r, i] >= tree)
                {
                    return count + 1;
                }
                count++;
            }
            return count;
        }
    }
}
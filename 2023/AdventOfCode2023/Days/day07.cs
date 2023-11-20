using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public class Day07 : BaseDay
    {
        public Node<Data> Root { get; set; }
        public long LongResult1 { get; set; }
        public long LongResult2 { get; set; }
        public List<Node<Data>> DirIndex { get; set; }

        public Day07 (string day, bool hasInput) : base(day, hasInput)
        {
            Root = new( new Data(""), null!);
            LongResult1 = 0;
            LongResult2 = 0;
            DirIndex = new();
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();
            BuildDirList();
            LongResult1 = SumDirSizesBySize(Root, 100000, 0);
            PrintResults(LongResult1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            _ = MapDirSizes(Root);
            List<Node<Data>> DirIndexSorted = DirIndex.OrderBy(x => x.Data.Size).ToList();
            long freeSpace = 30000000 - (70000000 - Root.Data.Size);
            LongResult2 = DirIndexSorted.SkipWhile(p => p.Data.Size <= freeSpace).First().Data.Size;
            PrintResults(LongResult2);
        }

        private void BuildDirList()
        {
            if (FileInput[0].Contains("$ cd /"))
            {
                Root.Data.SetId("/");
                _ = AddChild(Root, 1);
            }
        }

        private int AddChild(Node<Data> parent, int i)
        {
            if(i == FileInput.Count)
            {
                return 0;
            }
            if (FileInput[i].Contains("$ ls"))
            {
                i++;
                return AddChild(parent!, i);
            }
            if (FileInput[i].Contains("$ cd"))
            {
                if(FileInput[i].Equals("$ cd .."))
                {
                    parent = parent.Parent!;
                    i++;
                    return AddChild(parent!, i);
                }
                else
                {
                    parent = parent.Children.Find(x => x.Data.Id.Equals(string.Concat("dir ", FileInput[i].AsSpan(start: 5)), StringComparison.Ordinal));
                    i++;
                    return AddChild(parent!, i);
                }
            }

            Data data = new("");
            Node<Data> node = new(data, null!);

            if (FileInput[i].Contains("dir"))
            {
                data = new Data(FileInput[i]);
            }
            if (int.TryParse(FileInput[i].First().ToString(), out Int32 _))
            {
                var line = FileInput[i].Split(" ");
                data.SetSize(long.Parse(line[0]));
                data.SetId(line[1]);
            }

            node.SetData(data);
            node.SetParent(parent);
            parent.Children.Add(node);
            _ = AddFileSize(parent,data.GetSize());

            i++;
            return AddChild(parent!, i);
        }

        private int AddFileSize(Node<Data> p, long size)
        {
            if(p is null)
            {
                return 0;
            }
            p.Data.Size += size;
            return AddFileSize(p.Parent, size);
        }

        private long SumDirSizesBySize(Node<Data> p, long max, long sum)
        {
            if(p.Children.Count == 0)
            {
                return sum;
            }
            foreach (Node<Data> node in p.Children)
            {
                sum = SumDirSizesBySize(node, max, sum);
            }
            return p.Data.Size <= max ? sum += p.Data.Size : sum;
        }

        private int MapDirSizes(Node<Data> p)
        {
            if (p.Children.Count == 0)
            {
                return 0;
            }
            DirIndex.Add(p);
            foreach (Node<Data> node in p.Children)
            {
                MapDirSizes(node);
            }
            return 1;
        }
    }

    public class Node<T>
    {
        public List<Node<T>> Children { get; set; }
        public Node<T>? Parent { get; set; }
        public Data Data { get; set; }

        public Node (Data data, Node<T> parent)
        {
            Parent = parent;
            Children = new();
            Data = data;
        }

        public List<Node<T>> GetChildren() => Children;
        public Node<T>? GetParent() => Parent;
        public Data GetData() => Data;
        public void SetChildren(List<Node<T>> children) => Children = children;
        public void SetParent(Node<T> parent) => Parent = parent;
        public void SetData(Data data) => Data = data;
    }

    public class Data
    {
        public string Id { get; set; }
        public Int64 Size { get; set; }

        public Data(string id, long size = 0)
        {
            Id = id;
            Size = size;
        }

        public string GetId() => Id;
        public Int64 GetSize() => Size;
        public void SetId(string id) => Id = id;
        public void SetSize(long size) => Size = size;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Infrastructure
{
    public class Node<T>
    {
        public string Name { get; }

        public T Value { get; }

        public List<Node<T>> Children { get; private set; }

        public Node(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public void AddChild(Node<T> node) => Children.Add(node);

        public void AddChildren(IEnumerable<Node<T>> nodes) => Children.AddRange(nodes);

        public bool HasDescendent(string name)
            => Children.Any(node => node.Name == name || node.HasDescendent(name));
    }
}

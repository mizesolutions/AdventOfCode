using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Infrastructure
{
    public enum States
    {
        Floor,
        Occupied,
        Empty
    }

    public class Seat
    {
        public States PreviousState { get; set; }
        public States CurrentState { get; set; }
        public States TransitionState { get; set; }
        public List<(int row, int col)> AdjacentSeats { get; set; }
        public bool Changed { get; set; } 

        public void IsChanged()
        {
            Changed = !CurrentState.Equals(PreviousState);
        }

        public override string ToString()
        {
            return "Seat:\r\n" +
                   $"Curr: {CurrentState}\r\n" +
                   $"Tran: {TransitionState}\r\n" +
                   $"Prev: {PreviousState}\r\n" +
                   $"Adj#: {PrintAdjacentSeats()}\r\n" +
                   $"Changed: {Changed}\r\n";
        }
        private string PrintAdjacentSeats()
        {
            var temp = "{ ";
            for(var i =0; i < AdjacentSeats.Count; i++)
            {
                temp += $"({AdjacentSeats[i].row}, {AdjacentSeats[i].col})";
                if (i < AdjacentSeats.Count - 1)
                    temp += ", ";
            }
            temp += " }";
            return temp;
        }
    }
}

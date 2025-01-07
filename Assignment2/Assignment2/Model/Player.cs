using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment2.Model
{
    abstract class Player
    {
        public Disk Disk { get; set; }
        string Name { get; set; }

        public Player(string name, Disk disk)
        {
            this.Name = name;
            this.Disk = disk;
        }

        public abstract (int x, int y) RequestMove(GameBoard board, List<(int x, int y)> validMoves);
   
    }
}

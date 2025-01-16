using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Model
{
    internal class ComputerPlayer : Player
    {
        // Constructor
        public ComputerPlayer(string name, Disk disk) : base(name, disk)
        {
        }

        // Override RequestMove
        public override async Task<(int x, int y)> RequestMove(GameBoard board, List<(int x, int y)> validMoves)
        {
            //placeholder move
            (int x, int y) selectedMove = (-1, -1);

            Random random = new Random();
            int index = random.Next(validMoves.Count);

            selectedMove = validMoves[index];

            //Sleep for 2 seconds
            await Task.Delay(2000);

            // Return the selected move
            return selectedMove;

        }

    }
}
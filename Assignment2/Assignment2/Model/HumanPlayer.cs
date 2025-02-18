using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment2.Model
{
    class HumanPlayer : Player
    {
        public HumanPlayer(string name, Disk disk) : base(name, disk)
        {
        }

        public override async Task<(int x, int y)> RequestMove(GameBoard board, List<(int x, int y)> validMoves)
        {
            await Task.Delay(1); // Simulates async behavior
            return validMoves.Count > 0 ? validMoves[0] : (0, 0); // Returns the first valid move or (0,0) as fallback
        }
    }
}

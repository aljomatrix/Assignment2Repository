using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Model
{
    namespace Assignment2
    {
        public class ComputerPlayer : Player
        {
            private Disk _aiDisk;
            private string _aiName;

            public ComputerPlayer(string player2Name, Disk aiDisk) : base(player2Name, aiDisk)
            {
                this._aiDisk = aiDisk;
                this._aiName = player2Name;
            }

            public override async Task<(int x, int y)> RequestMove(GameBoard board, List<(int x, int y)> validMoves)
            {
                await Task.Delay(1); // Simulates async behavior
                return validMoves.Count > 0 ? validMoves[0] : (0, 0); // Returns the first valid move or (0,0) as fallback
            }

            internal async Task<(int x, int y)> ExecuteAIMove(GameBoard board)
            {
                // Get all valid moves for the AI
                List<(int x, int y)> validMoves = board.GetValidMoves(_aiDisk);

                // If no valid moves, return (-1, -1) to indicate pass
                if (validMoves.Count == 0)
                {
                    return (-1, -1);
                }

                // Simulate AI thinking time (optional)
                await Task.Delay(1000);

                // Select a move (Basic AI: Random move)
                Random random = new Random();
                (int x, int y) selectedMove = validMoves[random.Next(validMoves.Count)];

                // Execute the move
                board.ExecuteMove(selectedMove.x, selectedMove.y, _aiDisk);

                // Return the selected move
                return selectedMove;
            }
        }
    }
}
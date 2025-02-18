using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Model
{
    namespace Assignment2
    {
        public class ComputerPlayer
        {
            private Disk _aiDisk;
            private string _aiName;

            public ComputerPlayer(string player2Name, Disk aiDisk)
            {
                _aiDisk = aiDisk;
                this._aiName = player2Name;
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
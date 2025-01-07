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
        public override (int x, int y) RequestMove(GameBoard board, List<(int x, int y)> validMoves)
        {
            //placeholder move
            (int x, int y) selectedMove = (-1,-1);

            // Create a thread for the move calculation
            Thread moveThread = new Thread(() =>
            {
                Random random = new Random();
                int index = random.Next(validMoves.Count);

                selectedMove = validMoves[index];

                //Sleep for 2 seconds
                Thread.Sleep(2000);
            });

            // Start the thread and wait for it to finish
            moveThread.Start();
            moveThread.Join(); // Wait for the thread to finish before proceeding

            // Return the selected move
            return selectedMove;

        }

    }
}

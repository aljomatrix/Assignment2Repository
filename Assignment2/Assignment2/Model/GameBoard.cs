using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Assignment2.Model
{
    internal class GameBoard
    {

        private Disk[,] board = new Disk[8, 8];

        public Disk[,] BoardState
        {
            get { return board; }
        }

        private int BlackDiskCount
        {
            get
            {
                int count = 0;

                foreach (var cell in board)
                {
                    if (cell == Disk.Black)
                    {
                        count++;
                    }
                }
                return count;
            }
            
        }
        private int WhiteDiskCount
        {
            get
            {
                int count = 0;

                foreach (var cell in board)
                {
                    if (cell == Disk.White)
                    {
                        count++;
                    }
                }
                return count;
            }

        }


        private void resetBoard()
        {
            //Setting each cell to empty
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    board[row, col] = Disk.Empty;
                }
            }
        }

        public GameBoard()
        {
            //Reseting the board and setting up the initial starting position

            resetBoard();

            board[4, 4] = Disk.White;
            board[4, 5] = Disk.Black;
            board[5, 4] = Disk.Black;
            board[5, 5] = Disk.White;
        }

        

        public bool IsValidMove(int row, int col, Disk currentPlayerDisk)
        {
            // Check if the cell is within bounds and empty
            if (row < 0 || row >= 8 || col < 0 || col >= 8 || board[row, col] != Disk.Empty)
            {
                return false;
            }

            Disk opponentDisk;

            if(currentPlayerDisk == Disk.White)
            {
                opponentDisk = Disk.Black;
            }
            else if(currentPlayerDisk == Disk.Black)
            {
                opponentDisk = Disk.White;
            }

        }

        private bool ResultsInCapture(int row, int col, Disk currentPlayerDisk)
        {
            //horizontals
            if (board[row, col]  == currentPlayerDisk
        }


    }
}



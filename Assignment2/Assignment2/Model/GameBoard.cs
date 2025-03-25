using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Assignment2.Exceptions;

namespace Assignment2.Model
{
    public class GameBoard
    {

        private Disk[,] board = new Disk[8, 8];
        
        public Disk[,] BoardState
        {
            get { return board; }
        }

        public GameBoard()
        {
            //Reseting the board and setting up the initial starting position

            resetBoard();
            
            board[3, 3] = Disk.White;
            board[3, 4] = Disk.Black;
            board[4, 3] = Disk.Black;
            board[4, 4] = Disk.White;
        }
        public List<(int row, int col)> GetValidMoves(Disk currentPlayerDisk)
        {
            // Create a list to store valid moves
            List<(int row, int col)> validMoves = new List<(int, int)>();

            // Iterate through each cell on the board
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    // Check if the move at (row, col) is valid
                    if (IsValidMove(row, col, currentPlayerDisk))
                    {
                        // Add the valid move to the list
                        validMoves.Add((row, col));
                    }
                }
            }
            // Return the list of valid moves
            return validMoves;
        }

        public void ExecuteMove(int row, int col, Disk currentPlayerDisk)
        {
            if (currentPlayerDisk == Disk.Empty)
            {
                currentPlayerDisk = Disk.Black;
            }

            board[row, col] = currentPlayerDisk;

            int[,] directions = new int[,]
            {
                {-1, 0}, {1, 0}, {0, -1}, {0, 1},   // Up, Down, Left, Right
                {-1, -1}, {-1, 1}, {1, -1}, {1, 1}  // Diagonals
            };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dRow = directions[i, 0];
                int dCol = directions[i, 1];
                FlipDisksInDirection(row, col, dRow, dCol, currentPlayerDisk);
            }
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

        public int DiskCount(Disk DiskColor)
        {
            if(DiskColor == Disk.Black)
            {
                return BlackDiskCount;
            }
            else
            {
                return WhiteDiskCount;
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

        



        public bool IsValidMove(int row, int col, Disk currentPlayerDisk)
        {
            // Check if the cell is within bounds and empty
            if (row < 0 || row >= 8 || col < 0 || col >= 8 || board[row, col] != Disk.Empty)
            {
                return false;
            }

            Disk opponentDisk = DetermainOpponentDisk(currentPlayerDisk);

            //Definining each direction to check
            int[,] directions = new int[,]
            {
                {-1, 0}, {1, 0}, {0, -1}, {0, 1},   // Up, Down, Left, Right
                {-1, -1}, {-1, 1}, {1, -1}, {1, 1}  // Diagonals

            };

            for(int i = 0; i < directions.GetLength(0); i++)
            {
                int dRow = directions[i, 0];
                int dCol = directions[i, 1];

                int currentRow = row + dRow;
                int currentCol = col + dCol;

                bool hasOpponentDiskBetween = false;

                // Move in the direction until we either find a matching disk or go out of bounds
                while (currentRow >= 0 && currentRow < 8 &&  currentCol >= 0 && currentCol < 8)
                {
                    if (board[currentRow, currentCol] == opponentDisk)
                    {
                        hasOpponentDiskBetween = true;
                    }
                    else if(board[currentRow, currentCol] == currentPlayerDisk)
                    {
                        if(hasOpponentDiskBetween)
                        {
                            return true;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // If we hit an empty cell, no capture is possible in this direction
                        break;
                    }

                    // Move one step further in the current direction
                    currentRow += dRow;
                    currentCol += dCol;
                }
            }
            return false;
        }

       

        private void FlipDisksInDirection(int startRow, int startCol, int dRow, int dCol, Disk currentPlayerDisk)
        {
            // Determine the opponent's disk color
            Disk opponentDisk = DetermainOpponentDisk(currentPlayerDisk);

            // List to store the disks that will be flipped
            List<(int, int)> disksToFlip = new List<(int, int)>();

            // Start from the next cell in the direction (skip the clicked spot)
            int currentRow = startRow + dRow;
            int currentCol = startCol + dCol;

            // Iterate in the given direction
            while (currentRow >= 0 && currentRow < 8 && currentCol >= 0 && currentCol < 8)
            {
                // If we encounter an opponent's disk, add it to the list of disks to flip
                if (board[currentRow, currentCol] == opponentDisk)
                {
                    disksToFlip.Add((currentRow, currentCol));
                }
                // If we encounter the current player's disk, flip all the opponent's disks we've collected so far
                else if (board[currentRow, currentCol] == currentPlayerDisk)
                {
                    // If we have opponent disks to flip, do it
                    if (disksToFlip.Count > 0)
                    {
                        foreach (var (row, col) in disksToFlip)
                        {
                            board[row, col] = currentPlayerDisk; // Flip opponent disk to the current player's disk
                        }
                    }
                    return; // We've finished flipping in this direction, so return
                }
                // If we encounter an empty space, stop the search (no flip possible in this direction)
                else
                {
                    return;
                }

                // Move one step further in the current direction
                currentRow += dRow;
                currentCol += dCol;
            }
        }


        Disk DetermainOpponentDisk(Disk currentPlayerDisk)
        {
            if(currentPlayerDisk == Disk.Black)
            {
                return Disk.White;
            }
            else
            {
                return Disk.Black;
            }
        }
        public bool hasValidMoves(Disk playerDisk)
        {
            for(int col = 0; col < 8; col++)
            {
                for(int row = 0; row < 8; row++)
                {
                    if(IsValidMove(row, col, playerDisk) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool boardIsFull()
        {
            foreach(var cell in board)
            {
                if(cell == Disk.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        internal bool GameOver()
        {
            if (boardIsFull() || (hasValidMoves(Disk.White) == false && hasValidMoves(Disk.Black) == false))
            {
                return true;
            }
            else
                return false;
                
        }

    }

    
}



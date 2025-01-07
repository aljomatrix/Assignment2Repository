using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Assignment2.Exceptions;

namespace Assignment2.Model
{
    internal class GameBoard
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

            board[4, 4] = Disk.White;
            board[4, 5] = Disk.Black;
            board[5, 4] = Disk.Black;
            board[5, 5] = Disk.White;
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

       public void ExecuteMove(int row, int col, Disk currentPlayerDisk)
        {
            if (IsValidMove(row, col, currentPlayerDisk) == false)
            {
                throw new InvalidMoveException();
            }

            board[row, col] = currentPlayerDisk;

            Disk opponentDisk = DetermainOpponentDisk(currentPlayerDisk);

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

        private void FlipDisksInDirection(int startRow, int startCol, int dRow, int dCol, Disk currentPlayerDisk)
        {
            Disk opponentDisk = DetermainOpponentDisk(currentPlayerDisk);

            List<(int, int)> disksToFlip = new List<(int, int)>();

            int currentRow = startRow + dRow;
            int currentCol = startCol + dCol;

            while(currentRow >= 0 && currentRow < 8 && currentCol >= 0 && currentCol < 8)
            {
                if (board[currentRow, currentCol] == opponentDisk)
                {
                    disksToFlip.Add((currentRow, currentCol));
                }
                else if (board[currentRow, currentCol] == currentPlayerDisk)
                {
                    foreach (var (row, col) in disksToFlip)
                    {
                        board[row, col] = opponentDisk;
                    }
                    return;
                }
                else
                {
                    return;
                }

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



using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using Assignment2.Model.Assignment2;
using Assignment2.View;

namespace Assignment2.Model
{
    public class GameManager
    {
        private GameBoard _board;
        private Player _player1;
        private Player _player2;
        public GameBoard Board => _board;
        private Disk _currentPlayerDisk = Disk.Black;
        public delegate void UpdateBoardDelegate(Disk[,] board);
        public UpdateBoardDelegate OnUpdateBoard;

        public GameManager(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
            _board = new GameBoard();
        }

        public GameManager()
        {
            _board = new GameBoard();
        }

        public void ResetBoard()
        {
            _board = new GameBoard(); // Reinitialize the board to reset it
        }

        public void StartGame(GameGrid gameGridControl)
        {
            gameGridControl.UpdateBoard(_board.BoardState);
        }

        public GameBoard GetBoard()
        {
            return _board;
        }

        public bool IsValidMove(int row, int column)
        {
            return _board.IsValidMove(row, column, _currentPlayerDisk);
        }

        public async Task<bool> ExecuteMove(int row, int column)
        {
            if (row >= 0 && row < 8 && column >= 0 && column < 8)
            {
                if (_board.IsValidMove(row, column, _currentPlayerDisk))
                {
                    _board.ExecuteMove(row, column, _currentPlayerDisk);
                    OnUpdateBoard(_board.BoardState);
                    TogglePlayerTurn();

                    // If Player 2 is a computer, execute AI move
                    if (_player2 is ComputerPlayer computer)
                    {
                        await Task.Delay(1000); // Optional delay for realism
                        var aiMove = await computer.ExecuteAIMove(_board);
                        _currentPlayerDisk = Disk.White;
                        _board.ExecuteMove(aiMove.x, aiMove.y, _currentPlayerDisk);
                        OnUpdateBoard(_board.BoardState);
                        TogglePlayerTurn();
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Invalid move! Please try again.");
                    return false;
                }
            }
            return false;
        }
        private void TogglePlayerTurn()
        {
            // Toggle between Black and White
            if (_currentPlayerDisk == Disk.Black)
            {
                _currentPlayerDisk = Disk.White;
            }
            else if(_currentPlayerDisk == Disk.Empty)
            {
                _currentPlayerDisk = Disk.Black;
            }
            else
            {
                _currentPlayerDisk = Disk.Black;
            }
        }
        public void SetPlayers(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }
    }
}
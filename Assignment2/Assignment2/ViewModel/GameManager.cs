using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Assignment2.Model.Assignment2;
using Assignment2.View;

namespace Assignment2.Model
{
    public class GameManager
    {
        private GameBoard _board;
        private Player _player1;
        private Player _player2;

        public GameManager(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
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
    }
}
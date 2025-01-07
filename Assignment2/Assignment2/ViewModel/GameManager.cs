using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Assignment2.View;

namespace Assignment2.Model
{
    internal class GameManager
    {
        GameBoard Board;
        Player Player1;
        Player Player2;
        Player CurrentPlayer;

        public GameManager(Player player1, Player player2)
        {
            this.Board = new GameBoard();

            this.Player1 = player1;
            this.Player2 = player2;

            //Black starts the game
            if (player1.Disk == Disk.Black)
            {
                this.CurrentPlayer = Player1;
            }
            else
            {
                this.CurrentPlayer = Player2;
            }
        }
        public void StartGame()
        {
            while(Board.GameOver() == false)
            {
                if(Board.hasValidMoves(CurrentPlayer.Disk))
                {
                    //Creating a list of valid moves for the curent player
                    List<(int x, int y)> ValidMoves = Board.GetValidMoves(CurrentPlayer.Disk);

                    (int x, int y) tempMove = CurrentPlayer.RequestMove(Board, ValidMoves);

                    //Making sure that a valid move is chose, if not a new move is requested until it is valid
                    while (Board.IsValidMove(tempMove.x, tempMove.y, CurrentPlayer.Disk) == false)
                    {
                        tempMove = CurrentPlayer.RequestMove(Board, ValidMoves);
                    }

                    (int x, int y) chosenMove = tempMove;

                    Board.ExecuteMove(chosenMove.x, chosenMove.y, CurrentPlayer.Disk);

                    switchPlayer();
                }
                else
                {
                    //If the curent player has no valid moves, it becomes the next players turn
                    switchPlayer();
                }
            }
            if (Board.DiskCount(Player1.Disk) == Board.DiskCount(Player2.Disk))
            {
                //what to do here

            }
        }
        private void switchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == Player1) ? Player2 : Player1;
        }

        
    }
}

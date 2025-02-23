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
        public Player player1 { set; get; }
        public Player player2 { set; get; }


        public GameManager(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        public void StartGame(GameGrid gameGridControl)
        {

        }

    }
}
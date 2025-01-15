using System.Windows;

namespace Assignment2.View
{
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        // Event handler for the "New Game" button click
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the SetUpGameDialog to collect player names and types
            SetUpGameDialog setUpDialog = new SetUpGameDialog();
            bool? result = setUpDialog.ShowDialog();  // Show dialog and wait for result

            if (result == true)  // If OK was clicked (DialogResult = true)
            {
                // Retrieve player names and player types
                string player1Name = setUpDialog.Player1Name;
                string player2Name = setUpDialog.Player2Name;
                bool isPlayer2Computer = setUpDialog.IsPlayer2Computer;

                // Update the status section with the player names
                Player1Label.Text = "Player 1: " + player1Name;
                Player2Label.Text = "Player 2: " + player2Name;

                // You can set the scores or other initial data
                Player1Score.Text = "Black Disks: 0";
                Player2Score.Text = "White Disks: 0";

                // Initialize the game with the provided player data
                // For example:
                // GameManager.StartGame(player1Name, player2Name, isPlayer2Computer);
            }
            else
            {
                // If the user cancels the setup
                MessageBox.Show("Game setup was canceled.", "Canceled");
            }
        }

        // Event handler for the "Exit Game" button click
        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Application.Current.Shutdown();
        }
    }
}

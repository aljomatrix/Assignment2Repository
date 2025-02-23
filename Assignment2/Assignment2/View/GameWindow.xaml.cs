using Assignment2.Model;
using Assignment2.Model.Assignment2;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace Assignment2.View
{
    public partial class GameWindow : Window
    {
        private GameManager gameManager;
        public GameWindow()
        {
            InitializeComponent();
            InitializeStartingPieces();
        }
        private void InitializeStartingPieces()
        {
            PlacePiece(3, 3, "white_piece.png");
            PlacePiece(4, 4, "white_piece.png");
            PlacePiece(3, 4, "black_piece.png");
            PlacePiece(4, 3, "black_piece.png");
        }

        private void PlacePiece(int row, int col, string pieceImage)
        {

            var (x, y) = CellToPixelPosition(row, col);

            Image imageControl = new Image
            {
                Source = new BitmapImage(new Uri(pieceImage, UriKind.RelativeOrAbsolute)),
                Width = 50,
                Height = 50
            };

            // BoardGrid.Children.Add(imageControl);
        }

        private (int x, int y) CellToPixelPosition(int row, int col)
        {
            int cellWidth = 50;
            int cellHeight = 50;

            int x = col * cellWidth;
            int y = row * cellHeight;

            return (x, y);
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


                /* Creates 3 instances for the if statement. player 1 is always human 
                   so i always creater player1 as HumanPlayer and the if determines if it's computer or human.*/
                Player player1 = new HumanPlayer(player1Name, Disk.White);
                gameManager.player1 = player1;
                if (isPlayer2Computer)
                {
                    ComputerPlayer player2;
                    player2 = new ComputerPlayer(player2Name, Disk.Black);
                    gameManager.player2 = player2; ;
                }
                else
                {
                    Player player2;
                    player2 = new HumanPlayer(player2Name, Disk.Black);
                    gameManager.player2 = player2;
                }


                // Create the GameManager instance.
                GameManager game = new GameManager(player1Name, player2Name);
                game.StartGame(GameGridControl);

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

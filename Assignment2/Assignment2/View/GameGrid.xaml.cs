using Assignment2.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Assignment2.Model.Assignment2;



namespace Assignment2.View
{
    public partial class GameGrid : UserControl
    {
        GameBoard _board;
        public bool _isPlayer2Computer;
        private Disk _currentPlayerDisk = Disk.Black;
        public ComputerPlayer _computerPlayer;

        public GameGrid()
        {
            _board = new GameBoard();
            InitializeComponent();
            InitializeBoard();
            UpdateBoard(_board.BoardState);
        }
        public GameGrid(bool isPlayer2Computer)
        {
            InitializeComponent();
            _isPlayer2Computer = isPlayer2Computer;
            _board = new GameBoard();
            InitializeBoard();
            UpdateBoard(_board.BoardState);
        }

        // Mouse Click event handler
        private async void GameGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = e.GetPosition(BoardGrid);
            int column = (int)(mousePosition.X / 50);
            int row = (int)(mousePosition.Y / 50);

            if (row >= 0 && row < 8 && column >= 0 && column < 8)
            {
                if (_board.IsValidMove(row, column, _currentPlayerDisk))
                {
                    _board.ExecuteMove(row, column, _currentPlayerDisk);
                    UpdateBoard(_board.BoardState);
                    TogglePlayerTurn();

                    // If Player 2 is a computer, execute AI move
                    if (_isPlayer2Computer)
                    {
                        await Task.Delay(1000); // Optional delay for realism
                        var aiMove = await _computerPlayer.ExecuteAIMove(_board);
                        _board.ExecuteMove(aiMove.x, aiMove.y, _currentPlayerDisk);
                        UpdateBoard(_board.BoardState);
                        TogglePlayerTurn();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid move! Please try again.");
                }
            }

            if (_board.GameOver())
            {
                InitializeWinnerDialog();
            }
        }

        private void InitializeWinnerDialog()
        {
            // Determine the winner
            int blackCount = _board.DiskCount(Disk.Black);
            int whiteCount = _board.DiskCount(Disk.White);
            string winnerMessage = "Game Over! ";

            if (blackCount > whiteCount)
            {
                winnerMessage += "Black wins!";
            }
            else if (whiteCount > blackCount)
            {
                winnerMessage += "White wins!";
            }
            else
            {
                winnerMessage += "It's a tie!";
            }

            // Show the winner dialog
            WinnerDialog winnerDialog = new WinnerDialog(winnerMessage);
            winnerDialog.ShowDialog();

            // Close the application after the user acknowledges the result
            Application.Current.Shutdown();
        }
        private void TogglePlayerTurn()
        {
            // Toggle between Black and White
            if (_currentPlayerDisk == Disk.Black)
            {
                _currentPlayerDisk = Disk.White;
            }
            else
            {
                _currentPlayerDisk = Disk.Black;
            }
        }
        private void InitializeBoard()
        {
            // Clear any existing content (in case of reset)
            BoardGrid.Children.Clear();

            // Loop through each grid cell and create a tile
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    // Create the tile (Border)
                    var tile = new Border
                    {
                        Background = (row + column) % 2 == 0 ? Brushes.LightGreen : Brushes.DarkGreen,  // Alternating green shades
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1)
                    };

                    // Optionally, add an Ellipse for pieces (will be added later in the UpdateBoard method)
                    tile.Child = new Ellipse
                    {
                        Fill = Brushes.Transparent, // Pieces will be added later
                        Width = 40,
                        Height = 40,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    // Set row and column for the grid placement
                    Grid.SetRow(tile, row);
                    Grid.SetColumn(tile, column);

                    // Add the tile to the board grid
                    BoardGrid.Children.Add(tile);
                }
            }

            // Initialize the 4 starting ones with the right color

        }

        // This method will be called to update the board state dynamically (like placing pieces)
        public void UpdateBoard(Disk[,] boardState)
        {
            // Loop through the board state (2D array) and place pieces
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    // Find the corresponding Border in the BoardGrid
                    var tile = BoardGrid.Children[row * 8 + column] as Border;

                    // Ensure the Border is found and the Child is an Ellipse
                    var ellipse = tile?.Child as Ellipse;

                    // Update the Ellipse fill based on the board state
                    if (ellipse != null)
                    {
                        if (boardState[row, column] == Disk.White)
                        {
                            ellipse.Fill = Brushes.White; // Set piece to white
                        }
                        else if (boardState[row, column] == Disk.Black)
                        {
                            ellipse.Fill = Brushes.Black; // Set piece to black
                        }
                        else
                        {
                            ellipse.Fill = Brushes.Transparent; // Empty cell
                        }
                    }
                }
            }
        }
    }
}

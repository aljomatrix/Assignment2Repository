using Assignment2.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Assignment2.View
{
    public partial class GameGrid : UserControl
    {
        public GameGrid()
        {
            InitializeComponent();
            InitializeBoard();
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

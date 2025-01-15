using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Assignment2.View
{
    /// <summary>
    /// Interaction logic for GameGrid.xaml
    /// </summary>
    public partial class GameGrid : UserControl
    {
        // Event to notify the GameManager about the clicked position
        public event Action<int, int> TileClicked;

        public GameGrid()
        {
            InitializeComponent();
        }

        // MouseDown handler to determine which tile was clicked
        private void GameBoard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Get the position of the mouse click relative to the grid
            var position = e.GetPosition(GameBoard);

            // Determine which row and column were clicked
            int row = (int)(position.Y / (GameBoard.ActualHeight / 8)); // Assuming an 8x8 grid
            int column = (int)(position.X / (GameBoard.ActualWidth / 8));

            // Notify subscribers (e.g., GameManager)
            TileClicked?.Invoke(row, column);
        }

        // Update the visual state of the board (e.g., draw pieces)
        public void UpdateBoard(string[,] boardState)
        {
            GameBoard.Children.Clear(); // Clear previous state

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    var tile = new Border
                    {
                        Background = (row + column) % 2 == 0 ? Brushes.White : Brushes.Black, // Checkerboard pattern
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(1)
                    };

                    if (boardState[row, column] == "W")
                    {
                        tile.Child = new Ellipse
                        {
                            Fill = Brushes.White,
                            Width = 40,
                            Height = 40,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                    }
                    else if (boardState[row, column] == "B")
                    {
                        tile.Child = new Ellipse
                        {
                            Fill = Brushes.Black,
                            Width = 40,
                            Height = 40,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                    }

                    Grid.SetRow(tile, row);
                    Grid.SetColumn(tile, column);
                    GameBoard.Children.Add(tile);
                }
            }
        }
    }
}

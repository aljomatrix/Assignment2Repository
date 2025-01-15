using System.Windows;

namespace Assignment2.View
{
    public partial class DrawnDialog : Window
    {
        // Constructor that takes the draw message to display
        public DrawnDialog(string drawMessage)
        {
            InitializeComponent();
            DrawMessage.Text = drawMessage;  // Set the draw message
        }

        // Event handler for Close button click
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();  // Close the DrawnDialog
        }
    }
}

using System.Windows;

namespace Assignment2.View
{
    public partial class WinnerDialog : Window
    {
        // Constructor that takes the winner's message to display
        public WinnerDialog(string winnerMessage)
        {
            InitializeComponent();
            WinnerMessage.Text = winnerMessage; 
        }

        // Event handler for Close button click
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();  
        }
    }
}

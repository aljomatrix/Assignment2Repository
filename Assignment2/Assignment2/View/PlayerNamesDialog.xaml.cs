using System.Windows;

namespace Assignment2.View
{
    public partial class PlayerNamesDialog : Window
    {
        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }

        public PlayerNamesDialog()
        {
            InitializeComponent();
        }

        // Event handler for OK button click
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Capture the names from the text boxes
            Player1Name = Player1NameTextBox.Text;
            Player2Name = Player2NameTextBox.Text;

            if (string.IsNullOrEmpty(Player1Name) || string.IsNullOrEmpty(Player2Name))
            {
                MessageBox.Show("Please enter names for both players.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;  // Indicate that the dialog was successful (OK clicked)
            this.Close();
        }

        // Event handler for Cancel button click
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;  // Indicate that the dialog was canceled
            this.Close();
        }
    }
}

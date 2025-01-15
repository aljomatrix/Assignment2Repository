using System.Windows;

namespace Assignment2.View
{
    public partial class SetUpGameDialog : Window
    {
        // Properties to hold the player names and computer flag
        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }
        public bool IsPlayer2Computer { get; private set; }

        public SetUpGameDialog()
        {
            InitializeComponent();
        }

        // Event handler for OK button click
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Capture the player names and computer flag
            Player1Name = Player1NameTextBox.Text;

            // Check if Player 2 is a computer
            IsPlayer2Computer = (bool)IsPlayer2ComputerCheckBox.IsChecked;

            // If Player 2 is a computer, automatically set the name to "Computer"
            if (IsPlayer2Computer)
            {
                Player2Name = "Computer";
            }
            else
            {
                // Otherwise, get Player 2's name from the TextBox
                Player2Name = Player2NameTextBox.Text;
            }

            // Validation: Ensure both player names are provided (except Player 2 if it's a computer)
            if (string.IsNullOrEmpty(Player1Name) || (string.IsNullOrEmpty(Player2Name) && !IsPlayer2Computer))
            {
                MessageBox.Show("Please enter names for both players.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Set the DialogResult to true and close the dialog if everything is valid
            this.DialogResult = true;
            this.Close();
        }

        // Event handler for Cancel button click
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;  // Indicate that the dialog was canceled
            this.Close();
        }

        // Event handler for when the checkbox is checked
        private void IsPlayer2ComputerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // If Player 2 is a computer, automatically set Player 2's name to "Computer"
            Player2NameTextBox.IsEnabled = false; // Disable Player 2's text box
            Player2NameTextBox.Text = "Computer"; // Set the name to "Computer"
        }

        // Event handler for when the checkbox is unchecked
        private void IsPlayer2ComputerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Enable Player 2's text box for manual input
            Player2NameTextBox.IsEnabled = true;
            Player2NameTextBox.Clear(); // Clear the name field
        }
    }
}

using Client.Model.Services;
using Client.View.WindowFactory;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private IWindowFactory _windowFactory;
        private IAuthenticationService _authenticationService;

        public SignUpWindow(IWindowFactory windowFactory, IAuthenticationService authenticationService)
        {
            this._windowFactory = windowFactory;
            this._authenticationService = authenticationService;

            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string username = SignUpUsernameTextBox.Text;
            string password = SignUpPasswordTextBox.Text; // Consider hashing the password before storing
            string confirmPassword = SignUpConfirmPasswordTextBox.Text;
            string nickname = SignUpNicknameTextBox.Text;
            string accountType = ((ComboBoxItem)SignUpAccountTypeComboBox.SelectedItem)?.Content.ToString();

            // Perform validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(accountType))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Assuming validation passed, insert the new user into the database
            try
            {
                _authenticationService.CreateUser(username, password, nickname, accountType);
                MessageBox.Show("Account created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to insert data. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Proceed to log in window
            var logInWindow = _windowFactory.CreateLogInWindow();
            logInWindow.Show();
            this.Close();
        }
    }
}

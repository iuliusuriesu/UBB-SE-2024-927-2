using Client.Model.Services;
using Client.View.WindowFactory;
using System.Windows;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        private IWindowFactory _windowFactory;
        private IAuthenticationService _authenticationService;

        public LogInWindow(IWindowFactory windowFactory, IAuthenticationService authenticationService)
        {
            this._windowFactory = windowFactory;
            this._authenticationService = authenticationService;

            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text; // Consider hashing the password for comparison
            // string email = EmailTextBox.Text; // Uncomment if you use email as part of login
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_authenticationService.AuthenticateUser(username, password))
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // Proceed to the next window or dashboard
            }
            else
            {
                MessageBox.Show("Login failed. Please check your username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var drugMarketplaceWindow = _windowFactory.CreateDrugMarketplaceWindow(username);
            drugMarketplaceWindow.Show();
            this.Close();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var signUpWindow = _windowFactory.CreateSignUpWindow();
            signUpWindow.Show();
            this.Close();
        }
    }
}

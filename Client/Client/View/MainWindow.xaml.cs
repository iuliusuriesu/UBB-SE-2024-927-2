using Client.Model.Services;
using Client.View.WindowFactory;
using System.Windows;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IWindowFactory _windowFactory;
        private IAuthenticationService _authenticationService;
        private string _username;

        public MainWindow(IWindowFactory windowFactory, IAuthenticationService authenticationService, string username)
        {
            this._windowFactory = windowFactory;
            this._authenticationService = authenticationService;
            this._username = username;

            InitializeComponent();
        }

        private void DrugMarketplaceButton_Click(object sender, RoutedEventArgs e)
        {
            var drugMarketplaceWindow = _windowFactory.CreateDrugMarketplaceWindow(_username);
            drugMarketplaceWindow.Show();
            this.Close();
        }

        private async void AuctionsButton_Click(object sender, RoutedEventArgs e)
        {
            string userType = await _authenticationService.GetUserType(_username);
            if (userType == "admin")
            {
                var adminLiveAuctionWindow = _windowFactory.CreateAdminLiveAuctionWindow();
                adminLiveAuctionWindow.Show();
            }
            else
            {
                var userLiveAuctionWindow = _windowFactory.CreateUserLiveAuctionWindow();
                userLiveAuctionWindow.Show();
            }

            this.Close();
        }
    }
}

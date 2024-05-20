using Client.Model.Services;
using Client.View.WindowFactory;
using System.Windows;
using System;

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
            _authenticationService.GetUserType(_username).ContinueWith(userTypeTask => {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    // this is needed 
                    // otherwise we cannot create windows on threads
                    var drugMarketplaceWindow = _windowFactory.CreateDrugMarketplaceWindow(_username);
                    drugMarketplaceWindow.Show();
                    this.Close();
                });
            });
        }

        private void AuctionsButton_Click(object sender, RoutedEventArgs e)
        {
            _authenticationService.GetUserType(_username).ContinueWith(userTypeTask => {
                string userType=userTypeTask.Result;
                if (userType == "admin")
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        // this is needed 
                        // otherwise we cannot create windows on threads
                        var adminLiveAuctionWindow = _windowFactory.CreateAdminLiveAuctionWindow();
                        adminLiveAuctionWindow.Show();
                        this.Close();
                    });
                }
                else
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        // this is needed 
                        // otherwise we cannot create windows on threads
                        var userLiveAuctionWindow = _windowFactory.CreateUserLiveAuctionWindow();
                        userLiveAuctionWindow.Show();
                        this.Close();
                    });
                }
            });
        }
    }
}

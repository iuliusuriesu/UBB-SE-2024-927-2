using Client.Model.Services;
using Client.View.WindowFactory;
using System.Windows;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for ShoppingCartWindow.xaml
    /// </summary>
    public partial class ShoppingCartWindow : Window
    {
        private IWindowFactory _windowFactory;
        private IDrugMarketplaceService _drugMarketplaceService;
        private string _username;

        public ShoppingCartWindow(IWindowFactory windowFactory, IDrugMarketplaceService drugMarketplaceService, string username)
        {
            this._windowFactory = windowFactory;
            this._drugMarketplaceService = drugMarketplaceService;
            this._username = username;

            InitializeComponent();
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BackToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var drugMarketplaceWindow = _windowFactory.CreateDrugMarketplaceWindow(_username);
            drugMarketplaceWindow.Show();
            this.Close();
        }
    }
}

using Client.Model.Services;
using Client.View.WindowFactory;
using Client.ViewModel;
using System.Windows;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for ShoppingCartWindow.xaml
    /// </summary>
    public partial class ShoppingCartWindow : Window
    {
        private IWindowFactory _windowFactory;
        private IShoppingCartViewModel _shoppingCartViewModel;
        private string _username;

        public ShoppingCartWindow(IWindowFactory windowFactory, IShoppingCartViewModel shoppingCartViewModel, string username)
        {
            this._windowFactory = windowFactory;
            this._shoppingCartViewModel = shoppingCartViewModel;
            this._username = username;

            InitializeComponent();
            this.DataContext = _shoppingCartViewModel;
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

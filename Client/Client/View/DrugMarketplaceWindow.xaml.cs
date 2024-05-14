using Client.Model.Services;
using Client.View.WindowFactory;
using System;
using System.Windows;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for DrugMarketplaceWindow.xaml
    /// </summary>
    public partial class DrugMarketplaceWindow : Window
    {
        private IWindowFactory _windowFactory;
        private IDrugMarketplaceService _drugMarketplaceService;
        private string _username;

        public DrugMarketplaceWindow(IWindowFactory windowFactory, IDrugMarketplaceService drugMarketplaceService, string username)
        {
            this._windowFactory = windowFactory;
            this._drugMarketplaceService = drugMarketplaceService;
            this._username = username;

            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string text = SearchbarTextBox.Text;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DrugListListView.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
        }

        private void CartButton_Click_1(object sender, RoutedEventArgs e)
        {
            var shoppingCartWindow = _windowFactory.CreateShoppingCartWindow(_username);
            shoppingCartWindow.Show();
            this.Close();
        }
    }
}

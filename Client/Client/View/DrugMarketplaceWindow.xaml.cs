using Client.Model.Entities;
using Client.Model.Services;
using Client.View.WindowFactory;
using Client.ViewModel;
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
        private IProductViewModel _productViewModel;
        private string _username;

        public DrugMarketplaceWindow(IWindowFactory windowFactory, IDrugMarketplaceService drugMarketplaceService, IProductViewModel productViewModel, string username)
        {
            this._windowFactory = windowFactory;
            this._drugMarketplaceService = drugMarketplaceService;
            this._productViewModel = productViewModel;
            this._username = username;

            InitializeComponent();
            this.DataContext = _productViewModel;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string text = SearchbarTextBox.Text;
            _productViewModel.FilterProductsByName(text);
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DrugListListView.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }

            Product selectedProduct = (Product)selectedItem;
            try
            {
                _drugMarketplaceService.AddToShoppingCart(selectedProduct.ProductID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

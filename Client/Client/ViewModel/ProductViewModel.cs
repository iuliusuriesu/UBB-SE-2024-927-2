using Client.Model.Entities;
using Client.Model.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    public class ProductViewModel : IProductViewModel
    {
        private IDrugMarketplaceService _drugMarketplaceService;
        public ObservableCollection<Product> Products { get; set; }

        public ProductViewModel(IDrugMarketplaceService drugMarketplaceService)
        {
            _drugMarketplaceService = drugMarketplaceService;
            Products = new ObservableCollection<Product>();
            _drugMarketplaceService.FilterProductsByName(string.Empty).ContinueWith(products =>
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    // Products must be modified only on the main thread
                    foreach (var product in products.Result)
                        Products.Add(product);
                });
            });
        }

        public void FilterProductsByName(string text)
        {
            _drugMarketplaceService.FilterProductsByName(text).ContinueWith(productsTask =>
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    var products = productsTask.Result;
                    Products.Clear();
                    foreach (Product product in products)
                    {
                        Products.Add(product);
                    }
                });
            });
        }
    }
}

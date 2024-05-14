using Client.Model.Entities;
using Client.Model.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class ProductViewModel : IProductViewModel
    {
        private IDrugMarketplaceService _drugMarketplaceService;
        public ObservableCollection<Product> Products { get; set; }

        public ProductViewModel(IDrugMarketplaceService drugMarketplaceService)
        {
            _drugMarketplaceService = drugMarketplaceService;
            List<Product> products = _drugMarketplaceService.FilterProductsByName(string.Empty);
            Products = new ObservableCollection<Product>(products);
        }

        public void FilterProductsByName(string text)
        {
            List<Product> products = _drugMarketplaceService.FilterProductsByName(text);
            Products.Clear();
            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }
    }
}

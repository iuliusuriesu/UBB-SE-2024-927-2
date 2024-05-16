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
        }

        public static async Task<ProductViewModel> CreateAsync(IDrugMarketplaceService drugMarketplaceService)
        {
            var viewModel = new ProductViewModel(drugMarketplaceService);
            await viewModel.InitializeAsync();
            return viewModel;
        }

        private async Task InitializeAsync()
        {
            List<Product> products = await _drugMarketplaceService.FilterProductsByName(string.Empty);
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        public async void FilterProductsByName(string text)
        {
            List<Product> products = await _drugMarketplaceService.FilterProductsByName(text);
            Products.Clear();
            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }
    }
}

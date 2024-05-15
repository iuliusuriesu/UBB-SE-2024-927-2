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
    public class ShoppingCartViewModel : IShoppingCartViewModel
    {
        private IDrugMarketplaceService _drugMarketplaceService;
        public ObservableCollection<Product> Products { get; set; }

        public ShoppingCartViewModel(IDrugMarketplaceService drugMarketplaceService)
        {
            this._drugMarketplaceService = drugMarketplaceService;
            List<Product> products = _drugMarketplaceService.GetShoppingCart();
            Products = new ObservableCollection<Product>(products);
        }
    }
}

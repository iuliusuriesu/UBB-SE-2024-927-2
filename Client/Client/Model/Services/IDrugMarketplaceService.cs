using Client.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Services
{
    public interface IDrugMarketplaceService
    {
        void AddToShoppingCart(int productId);
        Task<List<Product>> FilterProductsByName(string text);
        List<Product> GetShoppingCart();
    }
}

using Client.Model.Entities;
using Client.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Services
{
    public class DrugMarketplaceService : IDrugMarketplaceService
    {
        private IUserRepository _userRepository;
        private IProductRepository _productRepository;
        private List<Product> _shoppingCart;

        public DrugMarketplaceService(IUserRepository userRepository, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _shoppingCart = new List<Product>();
        }

        public void AddToShoppingCart(int productId)
        {
            foreach (Product currentProduct in _shoppingCart)
            {
                if (currentProduct.ProductID == productId)
                {
                    throw new Exception("Product already added to cart!");
                }
            }

            Product product = _productRepository.GetProduct(productId);
            _shoppingCart.Add(product);
        }

        public List<Product> FilterProductsByName(string text)
        {
            List<Product> allProducts = _productRepository.GetAllProducts();
            if (text == string.Empty)
            {
                return allProducts;
            }

            List<Product> result = new List<Product>();
            text = text.ToLower();

            foreach (Product product in allProducts)
            {
                string productName = product.ProductName.ToLower();
                if (productName.Contains(text))
                {
                    result.Add(product);
                }
            }

            return result;
        }

        public List<Product> GetShoppingCart()
        {
            return _shoppingCart;
        }
    }
}

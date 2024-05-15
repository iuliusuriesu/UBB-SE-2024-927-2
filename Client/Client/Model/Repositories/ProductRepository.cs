using Client.Model.Entities;
using System.Collections.Generic;

namespace Client.Model.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> Products;

        public ProductRepository()
        {
            Products = new List<Product>
            {
                new Product(1, "Cocaine", "Bad drug", new decimal(23.45), "Drug", 5),
                new Product(2, "Xanax", "Another bad drug", new decimal(7.89), "Pill", 10),
            };
        }

        public List<Product> GetAllProducts()
        {
            // GET https://localhost:7100/api/Products
            return Products;
        }

        public Product GetProduct(int productId)
        {
            // GET https://localhost:7100/api/Products/:productId
            Product foundProduct = Products.Find(product => product.ProductID == productId);
            return foundProduct;
        }
    }
}

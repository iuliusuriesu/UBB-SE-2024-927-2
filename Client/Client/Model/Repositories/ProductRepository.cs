using Client.Model.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Client.Model.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository() { }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            // GET https://localhost:7100/api/Products
            using (var httpClient = new HttpClient())
            {
                string endpoint = "https://localhost:7100/api/Products";
                var result = await httpClient.GetAsync(endpoint);

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(json);
                }
                else
                {
                    throw new Exception($"Could not get products. Status code: {result.StatusCode}");
                }
            }

            return products;
        }

        public async Task<Product> GetProduct(int productId)
        {
            // GET https://localhost:7100/api/Products/:productId
            Product foundProduct = null;

            using (var httpClient = new HttpClient())
            {
                string endpoint = $"https://localhost:7100/api/Products/{productId}";
                var result = await httpClient.GetAsync(endpoint);

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    foundProduct = JsonConvert.DeserializeObject<Product>(json);
                }
                else
                {
                    throw new Exception($"Could not get product based on productId. Status code: {result.StatusCode}");
                }
            }

            return foundProduct;
        }
    }
}

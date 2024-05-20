using Client.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Model.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(int productId);
    }
}

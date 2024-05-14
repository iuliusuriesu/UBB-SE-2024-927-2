using Client.Model.Entities;
using System.Collections.Generic;

namespace Client.Model.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProduct(int productId);
    }
}

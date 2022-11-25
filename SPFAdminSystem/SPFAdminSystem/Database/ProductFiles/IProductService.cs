using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database.ProductFiles
{
    public interface IProductService
    {
        List<Product> GetProducts();

        Product GetInMemmorySingleProduct(string _);
        Task LoadProducts();

        Task CreateOrUpdateProduct(Product product);
        Task<Product> GetProductById(string prodId, bool forceUpdate = false);
    }
}
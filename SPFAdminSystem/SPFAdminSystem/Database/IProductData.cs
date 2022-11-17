using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database
{
    public interface IProductData
    {
        Task<List<Product>> GetProducts();
        Task InsertProduct(Product product);
    }
}
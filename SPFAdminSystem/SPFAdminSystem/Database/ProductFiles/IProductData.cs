using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database.ProductFiles
{
    public interface IProductData
    {
        Task<List<Product>> GetProducts();
        Task InsertProduct(Product product);
    }
}
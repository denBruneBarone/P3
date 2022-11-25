using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database.ProductFiles
{
    public interface IProductService
    {
        Task<Product> GetProductById(string prodId);
        List<Product> GetProducts();
        Task LoadProducts();
        Task<Product> GetSingleProduct(string productId);

        Task CreateOrUpdateProduct(Product product);

        Task InsertExcelProducts(string fileName);

        Task InsertExcelMapping(string fileName);
        Task CreateOrUpdateMapping(Mapping mapping);
        List<Mapping> GetMappings();
        Task LoadMappings();
        Task JoinMappingToProducts();
        Task AddToProduct(Mapping mapping);
    }
}
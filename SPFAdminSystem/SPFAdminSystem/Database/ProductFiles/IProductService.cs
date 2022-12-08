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
        Task<List<Product>> GetUnknownProducts(string fileName);
        Task LoadUnknownProducts(string fileName);
        Task<List<Product>> GetMatchSuggestions(Product product);
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
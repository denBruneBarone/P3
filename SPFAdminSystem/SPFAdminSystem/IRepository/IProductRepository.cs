using SPFAdminSystem.Data;

namespace SPFAdminSystem.IRepository
{
    public interface IProductRepository
    {
        List<Product> HandleExcel(string fileName);

    }
}

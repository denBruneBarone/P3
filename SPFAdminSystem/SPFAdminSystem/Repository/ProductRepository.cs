using OfficeOpenXml;
using SPFAdminSystem.Data;
using SPFAdminSystem.IRepository;

namespace SPFAdminSystem.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> HandleExcel(string fileName)
        {
            var FilePath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}" + "\\" + fileName;
            FileInfo fileInfo = new FileInfo(FilePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 5; row <= rowCount; row++)
                {
                    Product prod = new Product();
                    for(int col = 1; col <= colCount; col++)
                    {
                        if(worksheet.Cells[row, col].Value == null)
                        {
                            worksheet.Cells[row, col].Value = "empty";
                        }
                        Console.WriteLine(worksheet.Cells[row, col].Value.ToString());
                    }
                }

            }
            return new List<Product> { };
        }
    }
}

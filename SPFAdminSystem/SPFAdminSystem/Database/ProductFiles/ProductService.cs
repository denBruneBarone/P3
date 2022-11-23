using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SPFAdminSystem.Pages.DatabasePages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace SPFAdminSystem.Database.ProductFiles
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task LoadProducts()
        {
            Products = await _context.Products.ToListAsync();
        }

        public List<Product> GetProducts()
        {
            return Products;
        }

        public async Task<Product> GetSingleProduct(string productId)
        {
            Product? product = await _context.Products.FindAsync(productId);
            
            if (product == null)
            {
                throw new Exception("no product here");
            }
            return product;
        }

        public async Task CreateOrUpdateProduct(Product product)
        {
            var dbProduct = await _context.Products.FindAsync(product.ProductId);
            if (dbProduct == null)
            {
                //create
                _context.Products.Add(product);
            } else
            {
                // update /TOTEST
                dbProduct.ProductId = product.ProductId;
            }
            await _context.SaveChangesAsync();
        }

        public async Task InsertExcelProducts(string fileName)
        {
            List<Product> products = new();
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
                    prod.ProductId = worksheet.Cells[row, 1].Value.ToString();
                    prod.InHouseTitle = worksheet.Cells[row, 2].Value.ToString();
                    prod.AvailableAmount = Convert.ToInt32(worksheet.Cells[row, 9].Value);
                    prod.StockAmount = Convert.ToInt32(worksheet.Cells[row, 7].Value);
                    prod.OrderAmount = Convert.ToInt32(worksheet.Cells[row, 10].Value);
                    prod.Ordered = Convert.ToInt32(worksheet.Cells[row, 8].Value);
                    products.Add(prod);
                }
            }
            foreach (Product product in products)
            {
                _context.Products.Add(product);
            }
            await _context.SaveChangesAsync();
        }




        public async Task CreateOrUpdateMapping(Mapping mapping)
        {
            var dbMapping = await _context.Mappings.FindAsync(mapping.ProductId);
            if (dbMapping == null)
            {
                //create
                _context.Mappings.Add(mapping);
            }
            else
            {
                // update /TOTEST
                dbMapping.ProductId = mapping.ProductId;
            }
            await _context.SaveChangesAsync();
        }


        public async Task InsertExcelMapping(string fileName)
        {
            List<Mapping> Mapping = new();
            var FilePath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}" + "\\" + fileName;
            FileInfo fileInfo = new FileInfo(FilePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    Mapping Map = new Mapping();
                    if (Map.ProductId == null)
                    {
                        //create
                        Map.ProductId = worksheet.Cells[row, 2].Value == null ? string.Empty : worksheet.Cells[row, 2].Value.ToString();
                    }
                    else
                    {
                        // update /TOTEST
                        Map.ProductId = worksheet.Cells[row, 2].Value == null ? string.Empty : worksheet.Cells[row, 2].Value.ToString();
                    }

                    Map.TitleGWS = worksheet.Cells[row, 3].Value.ToString();
                    Map.Barcode = worksheet.Cells[row, 1].Value == null ? string.Empty : worksheet.Cells[row, 1].Value.ToString();
                    Map.Target = Convert.ToInt32(worksheet.Cells[row, 5].Value);
                    Map.MinOrder = Convert.ToInt32(worksheet.Cells[row, 7].Value);
                    Map.PackSize = Convert.ToInt32(worksheet.Cells[row, 6].Value);
                    Mapping.Add(Map);
                }
            }
            foreach (Mapping Map in Mapping)
            {
                _context.Mappings.Add(Map);
            }
            await _context.SaveChangesAsync();
        }

    }
}

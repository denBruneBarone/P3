﻿using System;
using System.Text;
using OfficeOpenXml;
using SPFAdminSystem.Data;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SPFAdminSystem.Pages.DatabasePages;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace SPFAdminSystem.Database.ProductFiles
{
    public class ProductScore
    {
        public double score { get; set; }
        public Product _product = new();
    }

    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new List<Product>();
        public List<Product> UnknownProducts { get; set; } = new List<Product>();
        public List<Mapping> Mappings { get; set; } = new List<Mapping>();
        public List<Product> MatchSuggestions { get; set; } = new List<Product>();

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

        public async Task CreateProduct(Product product, bool verify=true)
        {

            if (verify && _context.Products.FindAsync(product.ProductId).Result != null)
            {
                throw new Exception("Product already exists");
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProduct(Product product)
        {
            var dbProduct = await _context.Products.FindAsync(product.ProductId);
            if (dbProduct == null)
            {
                throw new KeyNotFoundException("product not found");
            }
            else
            {
               // update
                dbProduct.ArriveDate = product.ArriveDate;
                dbProduct.RemovedFromStockDate = product.RemovedFromStockDate;
                dbProduct.InHouseTitle = product.InHouseTitle;
                dbProduct.TitleGWS = product.TitleGWS;
                dbProduct.OrderPrice = product.OrderPrice;
                dbProduct.StockAmount = product.StockAmount;
                dbProduct.OrderAmount = product.OrderAmount;
                dbProduct.AvailableAmount = product.AvailableAmount;
                dbProduct.Ordered = product.Ordered;
                dbProduct.Barcode = product.Barcode;
                dbProduct.Packsize = product.Packsize;
                dbProduct.Target = product.Target;
                dbProduct.MinOrder = product.MinOrder;
                dbProduct.OrderQuantity = product.OrderQuantity;
            }

            await _context.SaveChangesAsync();
        }

        public async Task CreateOrUpdateProduct(Product product)
        {
            var dbProduct = await _context.Products.FindAsync(product.ProductId);
            if (dbProduct == null)
            {
                //create
                _context.Products.Add(product);
            }
            else
            {
                // update /TOTEST
                dbProduct.Barcode = product.Barcode;
                dbProduct.OrderPrice = product.OrderPrice;
                dbProduct.MinOrder = product.MinOrder;
                dbProduct.Ordered = product.Ordered;
                dbProduct.ArriveDate = product.ArriveDate;
                dbProduct.AvailableAmount = product.AvailableAmount;
                dbProduct.InHouseTitle = product.InHouseTitle;
                dbProduct.OrderAmount = product.OrderAmount;
                dbProduct.OrderQuantity = product.OrderQuantity;
                dbProduct.Packsize = product.Packsize;
                dbProduct.RemovedFromStockDate = product.RemovedFromStockDate;
                dbProduct.StockAmount = product.StockAmount;
                dbProduct.Target = product.Target;
                dbProduct.TitleGWS = product.TitleGWS;
            }
            await _context.SaveChangesAsync();
        }
        // same as function above, but does not save changes. There is no point in saving changes every time, when this is called repeatedly in the for-loop in InsertExcelProducts
        public async Task CreateOrUpdateProducts(Product product)
        {
            var dbProduct = await _context.Products.FindAsync(product.ProductId);
            if (dbProduct == null)
            {
                //create
                _context.Products.Add(product);
            }
            else
            {
                // update /TOTEST
                dbProduct.Barcode = product.Barcode;
                dbProduct.OrderPrice = product.OrderPrice;
                dbProduct.MinOrder = product.MinOrder;
                dbProduct.Ordered = product.Ordered;
                dbProduct.ArriveDate = product.ArriveDate;
                dbProduct.AvailableAmount = product.AvailableAmount;
                dbProduct.InHouseTitle = product.InHouseTitle;
                dbProduct.OrderAmount = product.OrderAmount;
                dbProduct.OrderQuantity = product.OrderQuantity;
                dbProduct.Packsize = product.Packsize;
                dbProduct.RemovedFromStockDate = product.RemovedFromStockDate;
                dbProduct.StockAmount = product.StockAmount;
                dbProduct.Target = product.Target;
                dbProduct.TitleGWS = product.TitleGWS;
            }
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
                package.Dispose();
            }
            foreach (Product product in products)
            {
                CreateOrUpdateProducts(product);
            }
            await _context.SaveChangesAsync();
        }


        public async Task CreateOrUpdateMapping(Mapping mapping)
        {
            var dbMapping = await _context.Mappings.FindAsync(mapping.ProductIdMapping);
            if (dbMapping == null)
            {
                //create
                _context.Mappings.Add(mapping);
            }
            /* else if (dbMapping.ProductIdMapping.ToString() == "N/A")
            {
            }*/
            else
            {
                // update /TOTEST
                dbMapping.Barcode = mapping.Barcode;
                dbMapping.MinOrder = mapping.MinOrder;
                dbMapping.PackSize = mapping.PackSize;
                dbMapping.Target = mapping.Target;
                dbMapping.TitleGWS = mapping.TitleGWS;
            }
        }

        public async Task InsertExcelMapping(string fileName)
        {
            List<Mapping> mappings = new();
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
                    
                    //create
                    Map.ProductIdMapping = (worksheet.Cells[row, 2].Value == null || worksheet.Cells[row, 2].Value == string.Empty) ? $"N/A{row}" : worksheet.Cells[row, 2].Value.ToString();
                    Map.TitleGWS = worksheet.Cells[row, 3].Value.ToString();
                    Map.Barcode = worksheet.Cells[row, 1].Value == null ? string.Empty : worksheet.Cells[row, 1].Value.ToString();
                    Map.Target = Convert.ToInt32(worksheet.Cells[row, 5].Value);
                    Map.MinOrder = Convert.ToInt32(worksheet.Cells[row, 7].Value);
                    Map.PackSize = Convert.ToInt32(worksheet.Cells[row, 6].Value);
                    mappings.Add(Map);

                }
                package.Dispose();
            }
            foreach (Mapping map in mappings)
            {
                CreateOrUpdateMapping(map);
            }

            await _context.SaveChangesAsync();
        }

        public async Task LoadMappings()
        {
            Mappings = await _context.Mappings.ToListAsync();
        }

        public List<Mapping> GetMappings()
        {
            return Mappings;
        }

        public async Task AddToProduct(Mapping mapping)
        {
            var dbProduct = await _context.Products.FindAsync(mapping.ProductIdMapping);

            if (dbProduct == null)
            {
                //Console.WriteLine("could not find product with id of : " + mapping.ProductIdMapping);
            }
            else
            {
                dbProduct.TitleGWS = mapping.TitleGWS;
                dbProduct.Barcode = mapping.Barcode;
                dbProduct.Packsize = mapping.PackSize;
                dbProduct.Target = mapping.Target;
                dbProduct.MinOrder = mapping.MinOrder;
            }
        }

        public async Task JoinMappingToProducts()
        {
            await LoadMappings();
            await LoadProducts();
            foreach (Mapping map in Mappings)
            {
                await AddToProduct(map);
            }
            await _context.SaveChangesAsync();
        }
        public async Task<Product> GetProductById(string prodId, bool forceUpdate = false)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(product => product.ProductId == prodId);
            if (prod == null)
                throw new KeyNotFoundException("product not found");

            if (forceUpdate)
            {
                _context.Entry<Product>(prod).Reload();
                ////Console.WriteLine("Updating...");
            }

            return prod;
        }

        public async Task<List<Product>> GetUnknownProducts(string fileName)
        {
            /*Calls function to assure products from database is in List<Product> Products*/
            UnknownProducts.Clear();

            await LoadMappings();
            await LoadUnknownProducts(fileName);
            return UnknownProducts;
        }

        public async Task LoadUnknownProducts(string fileName)
        {
            /*EPPLUS - Excel functionality*/
            var FilePath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}" + "\\" + @fileName;
            FileInfo fileInfo = new FileInfo(FilePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 3; row <= rowCount; row++)
                {
                    Product prod = new Product();
                    int isFound = 0;
                    foreach (Mapping mapping in Mappings)
                    {
                        string ExcelBarcode = worksheet.Cells[row, 6].Value.ToString();
                        if (mapping.Barcode.ToString() == ExcelBarcode)
                        {
                            isFound++;
                            break;
                        }

                    }
                    if (isFound == 0)
                    {
                        prod.ProductId = worksheet.Cells[row, 5].Value.ToString();
                        prod.Barcode = worksheet.Cells[row, 6].Value.ToString();
                        prod.TitleGWS = worksheet.Cells[row, 7].Value.ToString();
                        prod.Packsize = Convert.ToInt32(worksheet.Cells[row, 9].Value);
                        prod.OrderPrice = Convert.ToDouble(worksheet.Cells[row, 16].Value);

                        UnknownProducts.Add(prod);

                    }
                }
                //Console.WriteLine("Unknown Products added");
            }
        }

        public async Task<List<Product>> GetMatchSuggestions(Product product)
        {
            MatchSuggestions.Clear();
            List<ProductScore> prodScore = new List<ProductScore>();
            List<ProductScore> sortedProdScore = new();

            foreach (Mapping map in Mappings)
            {
                ProductScore prod = new();
                prod.score = NameMatch(product.TitleGWS, map.TitleGWS);
                prod._product.Target = map.Target;
                prod._product.TitleGWS = map.TitleGWS;
                prod._product.Barcode = map.Barcode;
                prod._product.Packsize = map.PackSize;
                prod._product.MinOrder = map.MinOrder;
                prod._product.ProductId = map.ProductIdMapping;
                prodScore.Add(prod);
            }
            sortedProdScore = prodScore.OrderByDescending(x => x.score).ToList();
            
            for(int i = 0; i<20; i++)
            {
                MatchSuggestions.Add(sortedProdScore[i]._product);
            }

            return MatchSuggestions;
        }

        static double NameMatch(string a, string b)
        {
            bool ignoreChecked = false;
            string[] ignoreWords = { " the ", " of ", " a ", " to ", " & " };
            /*a = a.ToLower();
            b = b.ToLower();*/

            bool bigScore = true;
            double stringLength = 0;
            string partString = "";
            string[] searchString = a.Split();
            string[] mappingString = b.Split();
            double score = 0;
            string charString = "";
            
            for (int i = 0; i < searchString.Length; i++)
            {
                charString = searchString[i];
                for (int j = 0; j < charString.Length; j++)
                {
                    if (charString[j] == ':')
                    {
                        charString = charString.Remove(j, 1);
                    }
                }
                searchString[i] = charString;
            }
            for (int i = 0; i < mappingString.Length; i++)
            {
                charString = mappingString[i];
                for (int j = 0; j < charString.Length; j++)
                {
                    if (charString[j] == ':')
                    {
                        charString = charString.Replace(":", "");
                    }
                }
                mappingString[i] = charString;
            }
            b = string.Join(" ", mappingString);
            a = string.Join(" ", searchString);

            stringLength = searchString.Length;
            stringLength = Math.Floor(stringLength/2);
            
            for(int x = 0; x < stringLength; x++)
            {
                partString += searchString[x] + " ";
            }

            if(b.Contains(partString, StringComparison.OrdinalIgnoreCase) && bigScore == true)
            {
                score = 1;
                bigScore = false;
            }
            else
            {
                bigScore = false;
            }

            for (int i = 0; i < searchString.Length; i++)
            {
                ignoreChecked = false;
                for (int j = 0; j < ignoreWords.Length; j++)
                {
                    if (searchString[i].Contains(ignoreWords[j], StringComparison.OrdinalIgnoreCase))
                    {
                        ignoreChecked = true;
                        break;
                    }
                }

                if (b.Contains(searchString[i], StringComparison.OrdinalIgnoreCase) && ignoreChecked == false)
                {
                    score++;
                }
            }

            return score;
        }
    }
}
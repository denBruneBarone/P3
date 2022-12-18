using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SPFAdminSystem.Pages.DatabasePages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
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


        public void CalculateProductOrderQuantity()
        {
            foreach (Product product in Products)
            {
                if (product.StockAmount < 0 || product.StockAmount == null)
                {
                    product.StockAmount = 0;
                }
                if (product.Ordered < 0 || product.Ordered == null)
                {
                    product.Ordered = 0;
                }
                if (product.Target < 0 || product.Target == null)
                {
                    product.Target = 0;
                }
                product.OrderQuantity = product.Target - product.StockAmount - product.Ordered;
                if (product.OrderQuantity < 0 || product.OrderQuantity == null)
                {
                    product.OrderQuantity = 0;
                }
                else
                {
                    product.OrderQuantity = product.OrderQuantity > product.MinOrder ? product.OrderQuantity : product.MinOrder;
                }
            }
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
            }
            await _context.SaveChangesAsync();
        }



        public async Task InsertExcelProducts(string fileName)
        {
            List<Product> products = new();
            var FilePath = $"C:\\Users\\peter\\source\\repos\\P3\\SPFAdminSystem\\SPFAdminSystem\\wwwroot\\" + fileName;
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
                CreateOrUpdateProduct(product);
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
/*            else if (dbMapping.ProductIdMapping.ToString() == "N/A")
            {

            }*/
            else
            {
                // update /TOTEST
                dbMapping.ProductIdMapping = mapping.ProductIdMapping;
                
            }
            await _context.SaveChangesAsync();
        }
         
        public async Task InsertExcelMapping(string fileName)
        {
            List<Mapping> mappings = new();
            var FilePath = $"C:\\Users\\peter\\source\\repos\\P3\\SPFAdminSystem\\SPFAdminSystem\\wwwroot\\" + fileName;
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
                    Console.WriteLine(Map.ProductIdMapping);
                    
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
                Console.WriteLine("could not find product with id of : " + mapping.ProductIdMapping);
            }
            else
            {
                dbProduct.TitleGWS = mapping.TitleGWS;
                dbProduct.Barcode = mapping.Barcode;
                dbProduct.Packsize = mapping.PackSize;
                dbProduct.Target = mapping.Target;
                dbProduct.MinOrder = mapping.MinOrder;
            }

            await _context.SaveChangesAsync();
        }

        public async Task JoinMappingToProducts()
        {
            await LoadMappings();
            await LoadProducts();
            foreach (Mapping map in Mappings)
            {
                await AddToProduct(map);
            }
        }
        public async Task<Product> GetProductById(string prodId)
        {
            var prod = await _context.Products.FindAsync(prodId);
            if (prod == null)
                throw new KeyNotFoundException("product not found");
            return prod;
        }

        public async Task<List<Product>> GetUnknownProducts(string fileName)
        {

            /*Calls function to assure products from database is in List<Product> Products*/
            await LoadMappings();
            await LoadUnknownProducts(fileName);
            return UnknownProducts;
        }


        public async Task LoadUnknownProducts(string fileName)
        {

            /*EPPLUS - Excel functionality*/
            var FilePath = $"C:\\Users\\peter\\source\\repos\\P3\\SPFAdminSystem\\SPFAdminSystem\\wwwroot\\" + @fileName;
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
                Console.WriteLine("Unknown Products added");
            }

        }
        public async Task<List<Product>> GetMatchSuggestions(Product product)
        {
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

            ProductScore newProduct = new();
            newProduct._product.TitleGWS = "new";
            sortedProdScore = prodScore.OrderByDescending(x => x.score).ToList();

            MatchSuggestions.Add(sortedProdScore[0]._product);
            MatchSuggestions.Add(sortedProdScore[1]._product);
            MatchSuggestions.Add(sortedProdScore[2]._product);
            MatchSuggestions.Add(sortedProdScore[3]._product);
            MatchSuggestions.Add(sortedProdScore[4]._product);
            MatchSuggestions.Add(newProduct._product);


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

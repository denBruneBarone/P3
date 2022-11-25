using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SPFAdminSystem.Pages.DatabasePages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

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

        public Product GetInMemmorySingleProduct(string productId)
        {
            Product? product = Products.Find(product => product.ProductId == productId);

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
                dbProduct.PackSize = product.PackSize;
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
                await CreateProduct(product, false);
            }
            else
            {
                //update
                await UpdateProduct(product);
            }
               
        }

        public async Task<Product> GetProductById(string prodId, bool forceUpdate=false)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(product => product.ProductId == prodId);
            if (prod == null)
                throw new KeyNotFoundException("product not found");

            if (forceUpdate)
            {
                _context.Entry<Product>(prod).Reload();
                //Console.WriteLine("Updating...");
            }

            return prod;
        }
    }
}

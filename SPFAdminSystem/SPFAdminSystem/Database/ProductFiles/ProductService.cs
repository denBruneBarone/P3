using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SPFAdminSystem.Pages.DatabasePages;
using System;
using System.Collections.Generic;
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
                dbProduct.Barcode = product.Barcode;
            }
            await _context.SaveChangesAsync();
        }

    }
}

using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database.ProductFiles
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _db;

        public ProductData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<Product>> GetProducts()
        {
            string sql = "select * from Products";

            return _db.LoadData<Product, dynamic>(sql, new { });
        }

        public Task InsertProduct(Product product)
        {
            string sql = "INSERT INTO Products (ProductId, InHouseTitle, Barcode, ArriveDate, AvailableAmount, OrderAmount, StockAmount, MinOrder, OrderPrice, OrderQuantity, Ordered, PackSize, RemovedFromStockDate, Target, TitleGWS)" +
                                       "VALUES (@ProductId, @InHouseTitle, @Barcode, @ArriveDate, @AvailableAmount, @OrderAmount, @StockAmount, @MinOrder, @OrderPrice, @OrderQuantity, @Ordered, @PackSize, @RemovedFromStockDate, @Target, @TitleGWS);";

            return _db.SaveData(sql, product);
        }
    }
}

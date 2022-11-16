﻿using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database
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
            string sql = "select * from dbo.Product";

            return _db.LoadData<Product, dynamic>(sql, new { });
        }

        public Task InsertProduct(Product product)
        {
            string sql = @"insert into dbo.Product (ProductId, InHouseTitle);";

            return _db.SaveData(sql, product);
        }
    }
}

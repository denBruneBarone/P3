﻿using DataAccessLibrary.Models;
using OfficeOpenXml;
using SPFAdminSystem.IRepository;
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
            string sql = "INSERT INTO Products (ProductId, InHouseTitle)" + "VALUES (@ProductId, @InHouseTitle);";

            return _db.SaveData(sql, product);
        }

        public Task InsertExcel(string fileName)
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
                    products.Add(prod);
                }
            }
            /* sql-streng får syntaks fra opslaget på StackOverflow:
            https://stackoverflow.com/questions/1609637/is-it-possible-to-insert-multiple-rows-at-a-time-in-an-sqlite-database */
            string sql = "INSERT INTO Products (ProductId, InHouseTitle)" + "VALUES ";
            foreach (Product product in products)
            {
                sql += $"(\"{product.ProductId}\", \"{product.InHouseTitle}\"),";
            }
            if (sql[sql.Length-1] == ',')
            {
                sql = sql.Substring(0, sql.Length - 1);
            }
            sql += ";";
            return _db.SaveDataNoParams<string>(sql);
        }
    }
}

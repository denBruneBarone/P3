﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Text;
using Aspose.Cells.Charts;
using BlazorTable;
using IronSoftware.Drawing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using SPFAdminSystem.Database.ProductFiles;
using SPFAdminSystem.Migrations;
using SPFAdminSystem.Pages.BlazorTemplatePages;
using SPFAdminSystem.Shared;
using Spire.Xls;

namespace SPF_UNITTESTS
{
    public class ProductServiceTests : TestContext
    {
        [Theory]
        [InlineData("Title", "Barcode1", "ProductId2", 1, 2, 3, 4, 5)]
        [InlineData("Title2", "Barcode2", "ProductId2", 2, 3, 4, 5, 6)]
        [InlineData("Title3", "Barcode3", "ProductId2", 3, 4, 5, 6, 7)]
        [InlineData("Title4", "Barcode4", "ProductId2", 4, 5, 6, 7, 8)]
        [InlineData("Title5", "Barcode5", "ProductId2", 5, 6, 7, 8, 9)]
        [InlineData("Title6", "Barcode6", "ProductId2", 6, 7, 8, 9, 10)]
        //Get Products should return a list of products.
        public void OverviewCorrectlyDisplaysProducts(string title, string barcode, 
                                                      string productid, int ordered, 
                                                      int minorder, int stockamount, 
                                                      int target, int orderquantity)
        {
            var mockService = new Mock<IProductService>();
            Product mockProduct = new Product()
            {
                TitleGWS = title,
                Barcode = barcode,
                ProductId = productid,
                Ordered = ordered,
                MinOrder = minorder,
                StockAmount = stockamount,
                Target = target,
                OrderQuantity = orderquantity 
            };
            mockService.Setup(m => m.GetProducts()).Returns(new List<Product> { mockProduct });

            Services.AddSingleton<IProductService>(mockService.Object);
            Services.AddBlazorTable();

            //RENDER
            var render = RenderComponent<Overview>();


            //Assert
            var TableData = render.FindAll("td");
            Assert.Equal(mockProduct.TitleGWS, TableData[0].GetInnerText());
            Assert.Equal(mockProduct.Barcode, TableData[1].GetInnerText());
            Assert.Equal(mockProduct.ProductId, TableData[2].GetInnerText());
            Assert.Equal(mockProduct.Ordered.ToString(), TableData[3].GetInnerText());
            Assert.Equal(mockProduct.MinOrder.ToString(), TableData[4].GetInnerText());
            Assert.Equal(mockProduct.StockAmount.ToString(), TableData[5].GetInnerText());
            Assert.Equal(mockProduct.Target.ToString(), TableData[6].GetInnerText());
            Assert.Equal(mockProduct.OrderQuantity.ToString(), TableData[7].GetInnerText());
        }

        [Fact]
        public void UploadDisplaysFileCardsCorrectly()
        {
            var mockService = new Mock<IProductService>();

            Services.AddSingleton<IWebHostEnvironment, myHostingEnvironment.HostingEnvironment>();
            Services.AddSingleton<IProductService>(mockService.Object);
            Services.AddBlazorTable();

            //RENDER
            var render = RenderComponent<Upload>();


            //Assert
            var comps = render.FindComponents<FileCard>();
            Assert.Equal(3, comps.Count);
            Assert.Equal("Produkter.xlsx was last modified on 12/12/2022 12.40.59", comps[0].Instance.Text);
            Assert.Equal("Mapping.xlsx was last modified on 12/12/2022 12.40.59", comps[1].Instance.Text);
            Assert.Equal("Pricelist Europe__3.xlsx was last modified on 12/12/2022 12.40.59", comps[2].Instance.Text);
        }

        [Fact]
        public void OrderQuantityIsCorrectlyCalculated()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "test").Options;
            var mockService = new Mock<ProductService>(new DataContext(options)).Object;
            List<Product> mockDb = new();
            Product mockProduct1 = new()
            {
                TitleGWS = "Title1",
                Barcode = "B1",
                Target = 2,
                OrderQuantity = 0,
                Ordered = 0,
                MinOrder = 1,
                StockAmount = 1
            };
            Product mockProduct2 = new()
            {
                TitleGWS = "Title2",
                Barcode = "B2",
                Target = 8,
                OrderQuantity = 0,
                MinOrder = 4,
                Ordered = 4,
                StockAmount = 1
            };
            Product mockProduct3 = new()
            {
                TitleGWS = "Title3",
                Barcode = "B3",
                Target = 5,
                OrderQuantity = 0,
                Ordered = 1,
                MinOrder = 1,
                StockAmount = 1
            };
            mockDb.Add(mockProduct1);
            mockDb.Add(mockProduct2);
            mockDb.Add(mockProduct3);
            mockService.Products = mockDb;


            //ACT
            mockService.CalculateProductOrderQuantity();

            //ASSERT
            Assert.Equal(1, mockDb[0].OrderQuantity);
            Assert.Equal(4, mockDb[1].OrderQuantity);
            Assert.Equal(3, mockDb[2].OrderQuantity);
        }
    }
}

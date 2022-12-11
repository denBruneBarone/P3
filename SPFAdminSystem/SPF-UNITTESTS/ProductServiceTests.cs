using System;
using System.Collections.Generic;
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
using Microsoft.Extensions.DependencyInjection;
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
        public void GetProductsReturnsAListOfProducts(string title, string barcode, string productid, int ordered, int minorder, int stockamount, int target, int orderquantity)
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
            Assert.Equal(mockProduct.TitleGWS, render.Find("td").GetInnerText());
        }

        [Fact]
        public void UploadDisplaysFileCardsCorrectly()
        {
            var mockService = new Mock<IProductService>();
            Product mockProduct = new Product()
            {
                TitleGWS = "title",
                Barcode = "barcode",
                ProductId = "id",
                Ordered = 1,
                MinOrder = 2,
                StockAmount = 3,
                Target = 4,
                OrderQuantity = 5
            };
            mockService.Setup(m => m.GetProducts()).Returns(new List<Product> { mockProduct });
            Services.AddSingleton<IWebHostEnvironment, myHostingEnvironment.HostingEnvironment>();
            Services.AddSingleton<IProductService>(mockService.Object);
            Services.AddBlazorTable();

            //RENDER
            var render = RenderComponent<Upload>();


            //Assert
            var comps = render.FindComponents<FileCard>();
            Assert.Equal(3, comps.Count);
        }
    }
}

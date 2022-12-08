using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPFAdminSystem.Database.ProductFiles;
using SPFAdminSystem.Pages.BlazorTemplatePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPF_UNITTESTS
{
    public class MergingTests
    {
        //TESTS IF THE MERGE PAGE RENDERS SUCCESSFULLY
        [Fact]
        public void MergeRenderSuccessfully()
        {
            //VARIABLES
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Path.Join(Environment.GetFolderPath(folder), "Spilforsyning.db");

            //ARRANGE
            using var ctx = new TestContext();
            ctx.Services.AddScoped<IWebHostEnvironment, myHostingEnvironment.HostingEnvironment>();
            ctx.Services.AddDbContext<DataContext>(options => options.UseSqlite($"Data Source={path}"));
            ctx.Services.AddScoped<IProductService, ProductService>();

            //RENDER
            var component = ctx.RenderComponent<MatchProducts>();


            //ASSERT
            Assert.Equal("Browse all products", component.Find($".btn").TextContent);
        }

        //TESTS IF MERGING CORRECTLY MERGES BARCODE, ORDERPRICE ETC..
        [Fact]
        public void MergeCorrectlyMerges()
        {
            //VARIABLES
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Path.Join(Environment.GetFolderPath(folder), "Spilforsyning.db");

            //ARRANGE
            using var ctx = new TestContext();
            ctx.Services.AddScoped<IWebHostEnvironment, myHostingEnvironment.HostingEnvironment>();
            ctx.Services.AddDbContext<DataContext>(options => options.UseSqlite($"Data Source={path}"));
            ctx.Services.AddScoped<IProductService, ProductService>();

            //RENDER
            var component = ctx.RenderComponent<MatchProducts>();
            var applyButton = component.Find($"#applybut");
            var ignoreButton = component.Find($"#ignorebut");
            var acceptButton = component.Find($"#acceptbut");
            var leftProduct = component.Find($"#productleftTitle");

            //ASSERT
            Assert.Equal("Browse all products", leftProduct.TextContent);


        }
    }
}

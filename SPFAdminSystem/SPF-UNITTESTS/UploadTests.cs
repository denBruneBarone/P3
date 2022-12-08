using Microsoft.Extensions.DependencyInjection;
using SPFAdminSystem.Database.ProductFiles;
using SPFAdminSystem.Database;
using SPFAdminSystem.Pages.BlazorTemplatePages;
using SPFAdminSystem.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using myHostingEnvironment;
using Moq;
using AngleSharp.Text;
using AngleSharp.Dom;

namespace SPF_UNITTESTS
{
    public class UploadingTests
    {
        [Fact]
        public void RendersSuccessfully()
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
            var component = ctx.RenderComponent<Upload>();


            //ASSERT
            Assert.Equal("Merge Mappings into Products' DB'", component.Find($".btn").TextContent);
        }

        [Fact]
        public void MergeAddsToDB()
        {
            //VARIABLES
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Path.Join(Environment.GetFolderPath(folder), "Spilforsyning.db");

            //EXCEPTED VALUES
            var dbCountBefore = 0;
            var dbCountAfter = 875;
            var mappingCountAfter = 2721;


            //ARRANGE
            using var ctx = new TestContext();
            ctx.Services.AddScoped<IWebHostEnvironment, myHostingEnvironment.HostingEnvironment>();
            ctx.Services.AddDbContext<DataContext>(options => options.UseSqlite($"Data Source={path}"));
            ctx.Services.AddScoped<IProductService, ProductService>();

            //RENDER
            var component = ctx.RenderComponent<Upload>();
            var mergeButton = component.Find($"#MERGEBUTTON");
            var mergeButtonText = "Merge Mappings into Products' DB'";


            //ASSERT
            Assert.Equal(mergeButtonText, mergeButton.TextContent );

            Assert.Equal(dbCountBefore, Int32.Parse(component.Find($"#productcounter").TextContent));
            Assert.Equal(dbCountBefore, Int32.Parse(component.Find("#mappingcounter").TextContent));
            mergeButton.Click();
            component.WaitForState(() => component.Find("#mergingstate").TextContent == "done", TimeSpan.FromMinutes(3));
            Assert.Equal(dbCountAfter, Int32.Parse(component.Find($"#productcounter").TextContent));
            Assert.Equal(mappingCountAfter, Int32.Parse(component.Find($"#mappingcounter").TextContent));
        }
    }
}
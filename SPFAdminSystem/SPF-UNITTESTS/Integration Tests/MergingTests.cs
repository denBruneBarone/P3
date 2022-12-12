using AngleSharp.Dom;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPFAdminSystem.Database.ProductFiles;
using SPFAdminSystem.Pages.BlazorTemplatePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
            var leftProduct = component.Find($".productleftTitleGWS");
            var rightProduct = component.Find($"#productrightTitleGWS");
            var continueButton = component.Find($"#continuebutton");
            var leftProductName = "NECROMUNDA: ORLOCK ASH WASTES DICE";
            var rightProductName = "Necromunda: Orlock Gang";
            var dbName = component.Find("#dbproductname");
            //ASSERT
            Assert.Equal("SQUAT IRONHEAD PROSPECTORS GANG DICE", leftProduct.TextContent);
            Assert.Equal("Necromunda: Ironhead Squat Prospectors", rightProduct.TextContent);

            ignoreButton.Click();
            component.WaitForState(() => leftProduct.TextContent == leftProductName, TimeSpan.FromSeconds(3));
            Assert.Equal(rightProductName, rightProduct.TextContent);
            applyButton.Click();
            component.WaitForState(() => acceptButton.IsDisabled() != true, TimeSpan.FromSeconds(2));
            acceptButton.Click();
            while(continueButton.IsDisabled())
            {
                Task.Delay(10);
                ignoreButton.Click();
            }
            // TODO:
            //
            // CHECK TITLE BEFORE CLICKING CONTINUE
            // CHECK AFTER CLICKING CONTINUE
            //
            Assert.False(continueButton.IsDisabled());
            Assert.Equal(leftProductName, dbName.TextContent);
            continueButton.Click();
            component.WaitForState(() => component.Find("#continuestatus").TextContent == "continuecomplete", TimeSpan.FromSeconds(10));
            Assert.Equal(rightProductName, dbName.TextContent);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPFAdminSystem.Database.ProductFiles;
using SPFAdminSystem.Database.UserFiles;
using SPFAdminSystem.Database;
using SPFAdminSystem.Pages.DatabasePages;
using var ctx = new TestContext();  
ctx.Services.AddTransient<IProductData, ProductData>();
ctx.Services.AddTransient<IUserData, UserData>();
ctx.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
namespace SPFAdminSystem.Tests
{
    public class UserActionsTests : TestContext { 

    // Register services
        [Fact]
        public void NumberOfRowsIsAsExpected()
        {
            // Arrange
            var cut = RenderComponent<UserActions>();

            // Assert that content of the paragraph shows counter at zero
            cut.Find("p").MarkupMatches("<p>Current count: 0</p>");
            var tableCells = cut.FindAll("td");
            // Assert that content of the paragraph shows counter at zero
            Assert.Equal(4, tableCells.Count);
        }

    }
}

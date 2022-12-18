using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPFAdminSystem.Database.ProductFiles;
using SPFAdminSystem.Database.UserFiles;
using SPFAdminSystem.Pages.DatabasePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace SPF_UNITTESTS
{
    public class UserManagementTests
    {
        [Fact]
        public void RenderCorrectly()
        {
            //VARIABLES
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Path.Join(Environment.GetFolderPath(folder), "Spilforsyning.db");

            //ARRANGE
            using var ctx = new TestContext();
            ctx.Services.AddScoped<IWebHostEnvironment, myHostingEnvironment.HostingEnvironment>();
            ctx.Services.AddDbContext<DataContext>(options => options.UseSqlite($"Data Source={path}"));
            ctx.Services.AddScoped<IUserService, UserService>();

            //RENDER
            var component = ctx.RenderComponent<Users>();


            //ASSERT
            Assert.Equal("Add User", component.Find($"#adduserbutton").TextContent);
        }

        [Fact]
        public async void AddUserAddsToDatabase()
        {
            //VARIABLES
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Path.Join(Environment.GetFolderPath(folder), "Spilforsyning.db");

            //ARRANGE
            using var ctx = new TestContext();
            ctx.Services.AddScoped<IWebHostEnvironment, myHostingEnvironment.HostingEnvironment>();
            ctx.Services.AddDbContext<DataContext>(options => options.UseSqlite($"Data Source={path}"));
            ctx.Services.AddScoped<IUserService, UserService>();

            //RENDER
            var component = ctx.RenderComponent<Users>();

            //EXPECTED VALUES
            var expectedUsername = "testuser";
            var expectedFullname = "testuserfullname";
            var expectedRole = "Employee";

            //Buttons + Inputs
            var AddUserButton = component.Find("#adduserbutton");
            var UserNameInput = component.Find("#usernameinput");
            var FullNameInput = component.Find("#fullnameinput");
            var RoleSelector = component.Find("#role");
            var PasswordInput = component.Find("#passwordinput");
            var ConfirmPasswordInput = component.Find("#cpasswordinput");
            var AddButton = component.Find("#adduserinput");

            var NewUserName = component.Find("#newusername");
            var NewUserFullName = component.Find("#newuserfullname");
            var NewUserRole = component.Find("#newuserrole");
            var NewUserPassword = component.Find("#newuserpassword");

            //ASSERT
            Assert.Equal(0, Int32.Parse(component.Find($"#usercount").TextContent));
            AddUserButton.Click();
            await Task.Delay(1);
            UserNameInput.Change("testuser");
            FullNameInput.Change("testuserfullname");
            RoleSelector.Change("Employee");
            PasswordInput.Change("password");
            ConfirmPasswordInput.Change("password");
            AddButton.Click();
            await Task.Delay(TimeSpan.FromSeconds(2));
            Assert.Equal(1, Int32.Parse(component.Find($"#usercount").TextContent));
            Assert.Equal(expectedUsername, NewUserName.TextContent);
            Assert.Equal(expectedFullname, NewUserFullName.TextContent);
            Assert.Equal(expectedRole, NewUserRole.TextContent);
            Assert.True(BCrypt.Net.BCrypt.Verify("password", NewUserPassword.TextContent));
        }
    }
}

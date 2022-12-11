using Aspose.Cells.Charts;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SPFAdminSystem.Database.ProductFiles;
using SPFAdminSystem.Database.UserFiles;
using SPFAdminSystem.Pages.DatabasePages;
using SPFAdminSystem.Shared.PopupModals;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPF_UNITTESTS
{
    public class UsersTest : TestContext
    {
        [Fact]
        public void AllUsersInDbAreDisplayed()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(m => m.GetUsers()).Returns(new List<User> {
                new User()
                {
                    UserId   = 1,
                    UserName = "TestUser1",
                    FullName = "FullNameUser1",
                    Role     = "Employee"
                },
                new User()
                {
                    UserId   = 2,
                    UserName = "TestUser2",
                    FullName = "FullNameUser2",
                    Role     = "Employee"
                },
                new User()
                {
                    UserId   = 3,
                    UserName = "TestUser3",
                    FullName = "FullNameUser3",
                    Role     = "Admin"
                }
            });
            Services.AddSingleton<IUserService>(mockService.Object);

            //RENDER
            var render = RenderComponent<Users>();


            //Assert
            render.MarkupMatches(@"

<h5>All Users</h5>
    <table class=""table table-striped"">
        <thead>
            <tr>
                <th style=""text-align: center;"">UserId</th>
                <th style=""text-align: center;"">UserName</th>
                <th style=""text-align: center;"">Password</th>
                <th style=""text-align: center;"">Full Name</th>
                <th style=""text-align: center;"">Role</th>
                <th style=""text-align: center;"">Edit</th>

            </tr>
        </thead>
        <tbody>
                <tr>
                    <td style=""text-align: center;"">1</td>
                    <td style=""text-align: center;"">TestUser1</td>
                    <td style=""text-align: center;"">****************</td>
                    <td style=""text-align: center;"">FullNameUser1</td>
                    <td style=""text-align: center;"">Employee</td>
                    <td style=""text-align: center;"">
                        <a href=""/users/1"">
                            <span class=""oi oi-pencil""></span>
                        </a>
                    </td>
                </tr>
                <tr>
                    <td style=""text-align: center;"">2</td>
                    <td style=""text-align: center;"">TestUser2</td>
                    <td style=""text-align: center;"">****************</td>
                    <td style=""text-align: center;"">FullNameUser2</td>
                    <td style=""text-align: center;"">Employee</td>
                    <td style=""text-align: center;"">
                        <a href=""/users/2"">
                            <span class=""oi oi-pencil""></span>
                        </a>
                    </td>
                </tr>
                <tr>
                    <td style=""text-align: center;"">3</td>
                    <td style=""text-align: center;"">TestUser3</td>
                    <td style=""text-align: center;"">****************</td>
                    <td style=""text-align: center;"">FullNameUser3</td>
                    <td style=""text-align: center;"">Admin</td>
                    <td style=""text-align: center;"">
                        <a href=""/users/3"">
                            <span class=""oi oi-pencil""></span>
                        </a>
                    </td>
                </tr>
        </tbody>
    </table>
    <div style=""width: 25%;"">
        <button type=""button"" class=""btn btn-primary"" id=""adduserbutton"">Add User</button>
    </div>  
<p id=""usercount"">3</p>
<p id=""newusername""></p>
<p id=""newuserfullname""></p>
<p id=""newuserrole""></p>");
        }

        [Fact]
        public void GetUserByIdReturnsTheCorrectUser()
        {
            List<User> users = new();

            User user1 = new()
            {
                UserId = 1,
                UserName = "Børge",
                FullName = "Børge Hansen",
                Role = "Admin"
            };
            User user2 = new()
            {
                UserId = 2,
                UserName = "John",
                FullName = "John Doe",
                Role = "Admin"
            };
            User user3 = new()
            {
                UserId = 3,
                UserName = "Jane",
                FullName = "Jane Doe",
                Role = "Employee"
            };
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);

            var mockService = new Mock<IUserService>();
            mockService.Setup(m => m.GetUserById(1)).Returns(Task.FromResult(users.Where(x => x.UserId == 1).First()));
            mockService.Setup(m => m.GetUserById(2)).Returns(Task.FromResult(users.Where(x => x.UserId == 2).First()));
            mockService.Setup(m => m.GetUserById(3)).Returns(Task.FromResult(users.Where(x => x.UserId == 3).First()));
            mockService.Setup(m => m.GetUsers()).Returns(users);
            Services.AddSingleton<IUserService>(mockService.Object);

            var cut = RenderComponent<EditUser>(parameters => parameters.Add(p => p.UserID, 2));
            cut.MarkupMatches(@"<h3><a href=""users"" class=""text-reset""><span class=""oi oi-arrow-circle-left""></span></a></h3>
<h3 class=""align-middle"">Editing User:  John Doe</h3>
<div class=""container informationContainer"">
    <div class=""info-column w-100"">
        <table class=""table table-striped table-hover border-info h-100"">
            <thead>
                <tr>
                    <th scope=""col"">Item</th>
                    <th scope=""col"">Value</th>
                    <th scope=""col"">Edit</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th class=""align-middle"" scope=""row"">UserID:</th>
                    <td class=""align-middle"" id=""tableuserid"">2</td>
                    <td class=""align-middle"" style=""width: 5%;""></td>
                </tr>
                <tr>
                    <th class=""align-middle"" scope=""row"">Username:</th>
                    <td class=""align-middle"">John</td>
                    <td class=""align-middle"" style=""width: 5%;"">
                        <button style=""all: unset; cursor: pointer;"">
                            <span class=""oi oi-pencil""></span>
                        </button>
                    </td>
                </tr>
                <tr>
                    <th class=""align-middle"" scope=""row"">Full Name:</th>
                    <td class=""align-middle"">John Doe</td>
                    <td class=""align-middle"" style=""width: 5%;"">
                        <button style=""all: unset; cursor: pointer;"">
                            <span class=""oi oi-pencil""></span>
                        </button>
                    </td>
                </tr>
                <tr>
                    <th class=""align-middle"" scope=""row"">Role:</th>
                    <td class=""align-middle"">Admin</td>
                    <td class=""align-middle"" style=""width: 5%;"">
                        <button style=""all: unset; cursor: pointer;"">
                            <span class=""oi oi-pencil""></span>
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class=""info-column w-50"">
        <div class=""h-50 w-75"" style=""display: flex; flex-direction: column; justify-content: space-around;"">
            <button type=""button"" class=""btn btn-info"">
                Change Password
            </button>
            <button type=""button"" class=""btn btn-danger"">
                DELETE USER
            </button>
        </div>
    </div>

</div>");

        }

        [Fact]

        public void EditUserDisplaysChangeFullNameWithCorrectUser()
        {
            List<User> users = new();

            User user1 = new()
            {
                UserId = 1,
                UserName = "Børge",
                FullName = "Børge Hansen",
                Role = "Admin"
            };
            User user2 = new()
            {
                UserId = 2,
                UserName = "John",
                FullName = "John Doe",
                Role = "Admin"
            };
            User user3 = new()
            {
                UserId = 3,
                UserName = "Jane",
                FullName = "Jane Doe",
                Role = "Employee"
            };
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);

            var mockService = new Mock<IUserService>();
            mockService.Setup(m => m.GetUserById(1)).Returns(Task.FromResult(users.Where(x => x.UserId == 1).First()));
            mockService.Setup(m => m.GetUsers()).Returns(users);
            Services.AddSingleton<IUserService>(mockService.Object);

            var cut = RenderComponent<EditUser>(parameters => parameters.Add(p => p.UserID, 1));

            var fullnamepopup = cut.FindComponent<ChangeFullnamePopup>();
            Assert.Equal(fullnamepopup.Instance.user, users[0]);
        }
    }
}

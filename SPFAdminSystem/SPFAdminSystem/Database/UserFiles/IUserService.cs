using DataAccessLibrary.Models;
using SPFAdminSystem.Pages.DatabasePages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database.UserFiles
{
    public interface IUserService
    {


        Task LoadUsers();

        List<User> GetUsers();

        Task InsertUser(User user);
        Task<User> GetUserByName(string username);

    }
}
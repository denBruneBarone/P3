using DataAccessLibrary.Models;
using SPFAdminSystem.Pages.DatabasePages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database.UserFiles
{
    public interface IUserService
    {

        Task InsertAction(UserAction action);
        Task<List<UserAction>> GetActions();

        Task LoadUsers();

        List<User> GetUsers();

        Task InsertUser(User user);
        Task<User> GetUserById(int UserId);
        Task<User> GetUserByName(string username);

    }
}
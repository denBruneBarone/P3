using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database.UserFiles
{
    public interface IUserData
    {
        Task<List<User>> GetUsers();
        Task<List<UserAction>> GetActions();
        Task InsertUser(User user);
        Task InsertAction(UserAction action);

        Task<User> GetUserByName(string UserName);
    }
}
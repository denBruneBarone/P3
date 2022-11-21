using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database.UserFiles
{
    public interface IUserData
    {
        Task<List<User>> GetUsers();
        Task InsertUser(User user);
        Task<User> GetUserByName(string UserName);
    }
}
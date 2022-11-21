using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database.UserFiles
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _db;

        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<User>> GetUsers()
        {
            string sql = "select * from Users";

            return _db.LoadData<User, dynamic>(sql, new { });
        }

        public Task InsertUser(User user)
        {
            string sql = "INSERT INTO Users (UserName, Password, FullName, Role)" + "VALUES (@UserName, @Password, @FullName, @Role);";

            return _db.SaveData(sql, user);
        }

        public Task<User> GetUserByName(string UserName)
        {
            string sql = $"SELECT * FROM 'Users' WHERE UserName='{UserName}';";

            return _db.GetSingleData<User>(sql);
        }
    }
}

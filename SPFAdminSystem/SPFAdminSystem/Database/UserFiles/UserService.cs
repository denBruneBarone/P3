using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SPFAdminSystem.Pages.DatabasePages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database.UserFiles
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public List<User> Users { get; set; } = new List<User>();


        public async Task LoadUsers()
        {
            Users = await _context.Users.ToListAsync();
        }

        


        public List<User> GetUsers()
        {
            return Users;
        }

        public async Task InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByName(string UserName)
        {
            var dbUser = await _context.Users.FindAsync(new Dictionary<string, object> { { "UserName", UserName } });
            if (dbUser == null)
            {
                throw new Exception("no user here");
            }
            return dbUser;
        }
    }
}

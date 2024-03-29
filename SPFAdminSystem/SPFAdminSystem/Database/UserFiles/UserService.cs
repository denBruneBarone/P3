﻿using DataAccessLibrary.Models;
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

        public List<User> Users { get; set; } = new List<User>();
        public List<UserAction> UserActions { get; set; } = new List<UserAction>();
        public UserService(DataContext context)
        {
            _context = context;
        }
        public async Task LoadUsers()
        {
            Users = await _context.Users.ToListAsync();
        }
        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
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

        public async Task<User> GetUserByName(string username)
        {
            var dbUser = await _context.Users.Where(x => x.UserName == username).FirstAsync();
            if (dbUser == null)
            {
                throw new Exception("no user here");
            }
            return dbUser;
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task LoadActions()
        {
            UserActions = await _context.UserActions.ToListAsync();
        }
        public List<UserAction> GetActions()
        {
            return UserActions;
        }

        public async Task InsertAction(UserAction action)
        {
            _context.UserActions.Add(action);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            var dbUser = await _context.Users.Where(x=>x.UserId== id).FirstAsync();
            if (dbUser == null) throw new Exception("User not found");
            return dbUser;
        }

    }
}

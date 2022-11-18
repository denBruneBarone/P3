namespace SPFAdminSystem.Authentication
{
    public class UserAccountService
    {
        public List<UserAccount> Users;

        public UserAccountService()
        {
            Users = new List<UserAccount>
            {
                new UserAccount{ UserName = "admin", Password = "admin", Role = "Administrator" },
                new UserAccount{ UserName = "user", Password = "user", Role = "User" }
            };
        }

        public UserAccount? GetByUserName(string userName)
        {
            return Users.FirstOrDefault(x => x.UserName == userName);
        }
        public void Add(UserAccount user)
        {
            if(user.UserName == null || user.Password == null || user.Role == null)
            {
                throw new ArgumentNullException();
            }
            Users.Add(user);
        }
    }
}

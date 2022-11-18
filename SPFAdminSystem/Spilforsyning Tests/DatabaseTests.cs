using SPFAdminSystem.Database.UserFiles;
using SPFAdminSystem.Database;

namespace Spilforsyning_Tests

{
    public class DatabaseTests
    {
        [Fact]
        public void GetUserByString()
        {
            // Arrange
            string UserName = "peter";

            // Act
            UserData db;
            Task result = db.GetUserByName(UserName);
            // Assert


        }
    }
}
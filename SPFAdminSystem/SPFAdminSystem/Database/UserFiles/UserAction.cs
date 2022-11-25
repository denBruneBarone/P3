using SPFAdminSystem.Database.ProductFiles;
using SPFAdminSystem.Database.UserFiles;
using System.ComponentModel.DataAnnotations;

public class UserAction
{
    public int UserActionId { get; set; }

    [Required]
    public int UserId { get; set; }

    public User User { get; set; }

    public DateTime Date { get; set; }

    public string ActionType { get; set; }

    public string Value { get; set; }

    public string? ProductId { get; set; }

    public Product Product { get; set; }
    public UserAction() { }

    public UserAction(int userId, User user, DateTime date, string actionType, string value, string productId, Product product)
    {
        UserId = userId;
        User = user;
        Date = date;
        ActionType = actionType;
        Value = value;
        ProductId = productId;
        Product = product;
    }

    public async Task<UserAction> CreateUserAction(string userName, string identifier, IUserService userDb, IProductService prodDb)
    {
        User currUser = await userDb.GetUserByName(userName);
        UserAction action;
        if (identifier == "Excel File Upload")
        {
            //dev
            string prodId = "5011921003747";
            Product prod = await prodDb.GetProductById(prodId);

            action = new UserAction(currUser.UserId, currUser, DateTime.Now, "Excel File Upload", "", prodId, prod);
        }
        else
        {
            Product prod = await prodDb.GetProductById(identifier);
            action = new UserAction(currUser.UserId, currUser, DateTime.Now, "OrderQuantity", "7", identifier, prod);
        }

        await userDb.InsertAction(action);
        return action;
    }
}
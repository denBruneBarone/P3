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

    public string ProductId { get; set; }

    public Product Product { get; set; }
    public UserAction(int userId, DateTime date, string actionType, string value, string productId)
    {
        UserId = userId;
        Date = date;
        ActionType = actionType;
        Value = value;
        ProductId = productId;
    }
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
}
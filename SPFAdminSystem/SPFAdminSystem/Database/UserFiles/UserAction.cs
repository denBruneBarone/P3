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
}
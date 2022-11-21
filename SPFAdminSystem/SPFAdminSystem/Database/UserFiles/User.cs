using System.ComponentModel.DataAnnotations;

public class User
{
    public int UserId { get; set; }

    public List<UserAction> UserActions { get; set; }

    public DateTime? RegisterDate { get; set; }

    public string? CreatedBy { get; set; }

    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? FullName { get; set; }

    public DateTime? DeleteDate { get; set; }

    [Required]
    public string Role { get; set; }
}

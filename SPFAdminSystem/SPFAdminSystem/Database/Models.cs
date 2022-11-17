using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class SpilforsyningContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<UserAction> UserActions { get; set; }

    public string DbPath { get; }

    public SpilforsyningContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "Spilforsyning.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
}

public class User
{
    public int UserId { get; set; }

    public List<UserAction> UserActions { get; set; }

    public DateTime RegisterDate { get; set; }

    [Required]
    public string CreatedBy { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string FullName { get; set; }

    public DateTime DeleteDate { get; set; }

    [Required]
    public string Role { get; set; }
}

public class Product
{
    public DateTime? ArriveDate { get; set; }

    public DateTime? RemovedFromStockDate { get; set; }

    [Required]
    [Key]
    public string ProductId { get; set; }

    public List<UserAction> UserActions { get; set; }

    [Required]
    public string InHouseTitle { get; set; }

    public string? TitleGWS { get; set; }

    public double? OrderPrice { get; set; }

    public int? StockAmount { get; set; }

    public int? OrderAmount { get; set; }

    public int? AvailableAmount { get; set; }

    public int? Ordered { get; set; }

    public int? Barcode { get; set; }

    public int? PackSize { get; set; }

    public int? Target { get; set; }

    public int? MinOrder { get; set; }

    public int? OrderQuantity { get; set; }
}

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
}
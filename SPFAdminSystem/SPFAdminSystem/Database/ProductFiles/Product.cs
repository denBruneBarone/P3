using System.ComponentModel.DataAnnotations;

public class Product
{
    public DateTime? ArriveDate { get; set; }

    public DateTime? RemovedFromStockDate { get; set; }

    [Required]
    [Key]
    public string ProductId { get; set; }

    public List<UserAction> UserActions { get; set; }

    public string? InHouseTitle { get; set; }

    public double? OrderPrice { get; set; }

    public int? StockAmount { get; set; }

    public int? OrderAmount { get; set; }

    public int? AvailableAmount { get; set; }

    public int? Ordered { get; set; }

    public int? OrderQuantity { get; set; }

    public string? TitleGWS { get; set; }

    public string? Barcode { get; set; }

    public int? Packsize { get; set; }

    public int? MinOrder { get; set; }

    public int? Target { get; set; }
}

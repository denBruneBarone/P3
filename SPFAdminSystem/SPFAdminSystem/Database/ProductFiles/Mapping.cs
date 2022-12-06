using System.ComponentModel.DataAnnotations;

public class Mapping
{
    [Required]
    [Key]
    public string ProductIdMapping { get; set; }

    public string? Barcode { get; set; }

    [Required]
    public string TitleGWS { get; set; }

    public int? PackSize { get; set; }

    public int? MinOrder { get; set; }

    public int? Target { get; set; }
}
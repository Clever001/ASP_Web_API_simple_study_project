using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Dtos.Stock;

public class CreateStockDto {
    [Required]
    [Length(1, 10)]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [Length(1, 10)]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1, 1_000_000_000_000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDir { get; set; }
    [Required]
    [Length(1, 10)]
    public string Industry { get; set; } = string.Empty;
    [Required]
    [Range(1, 5_000_000_000_000)]
    public long MarketCap { get; set; }
}
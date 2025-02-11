using WebApplication3.Dtos.Comment;

namespace WebApplication3.Dtos.Stock;

public class StockDto {
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDir { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
    public List<CommentShortDto> Comments { get; set; }
}
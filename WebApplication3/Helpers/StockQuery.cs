namespace WebApplication3.Helpers;

public class StockQuery {
    public string? Symbol { get; set; } = null;
    public string? CompanyName { get; set; } = null;
    public string? OrderBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
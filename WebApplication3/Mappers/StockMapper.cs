using api.Models;
using WebApplication3.Dtos.Stock;

namespace WebApplication3.Mappers;

public static class StockMapper {
    public static StockDto ToStockDto(this Stock stock) {
        return new StockDto {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDir = stock.LastDir,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap
        };
    }
}
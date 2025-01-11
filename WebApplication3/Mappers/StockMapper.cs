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

    public static Stock ToStockFromCreateDto(this CreateStockDto createStockDto) {
        return new Stock {
            Symbol = createStockDto.Symbol,
            CompanyName = createStockDto.CompanyName,
            Purchase = createStockDto.Purchase,
            LastDir = createStockDto.LastDir,
            Industry = createStockDto.Industry,
            MarketCap = createStockDto.MarketCap
        };
    }

    public static void Update(this Stock stock, UpdateStockDto updateStockDto) {
        stock.Symbol = updateStockDto.Symbol;
        stock.CompanyName = updateStockDto.CompanyName;
        stock.Purchase = updateStockDto.Purchase;
        stock.LastDir = updateStockDto.LastDir;
        stock.Industry = updateStockDto.Industry;
        stock.MarketCap = updateStockDto.MarketCap;
    }
}
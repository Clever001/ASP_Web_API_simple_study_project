using api.Models;
using WebApplication3.Dtos.Stock;
using WebApplication3.Helpers;

namespace WebApplication3.Interfaces;

public interface IStockRepository {
    Task<List<Stock>> GetAllAsync(StockQuery query);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stock);
    Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExists(int id);
}
using api.Models;
using WebApplication3.Dtos.Stock;

namespace WebApplication3.Interfaces;

public interface IPortfolioRepository {
    public Task<List<StockDto>> GetStocksAsync(AppUser user);
}
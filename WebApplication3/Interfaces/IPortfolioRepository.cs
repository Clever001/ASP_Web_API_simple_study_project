using api.Models;
using WebApplication3.Dtos.Stock;

namespace WebApplication3.Interfaces;

public interface IPortfolioRepository {
    public Task<List<Stock>> GetStocksAsync(AppUser user);
    public Task<bool> ContainsStockAsync(AppUser user, Stock stock);
    public Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);
    public Task<Portfolio?> DeletePortfolioAsync(AppUser user, Stock stock);
}
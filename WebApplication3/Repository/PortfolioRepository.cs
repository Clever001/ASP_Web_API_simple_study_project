using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Dtos.Stock;
using WebApplication3.Interfaces;
using WebApplication3.Mappers;

namespace WebApplication3.Repository;

public class PortfolioRepository : IPortfolioRepository {
    private ApplicationDbContext _context;

    public PortfolioRepository(ApplicationDbContext context) {
        _context = context;
    }
    
    public async Task<List<StockDto>> GetStocksAsync(AppUser user) {
        return await _context.Portfolios.Where(p => p.AppUserId == user.Id)
            .Select(p => new StockDto {
                Comments = new(),
                CompanyName = p.Stock.CompanyName,
                Id = p.Stock.Id,
                Industry = p.Stock.Industry,
                LastDir = p.Stock.LastDir,
                MarketCap = p.Stock.MarketCap,
                Purchase = p.Stock.Purchase,
                Symbol = p.Stock.Symbol,
            }).ToListAsync();
    }
}
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
    
    public async Task<List<Stock>> GetStocksAsync(AppUser user) {
        return await _context.Portfolios.Where(p => p.AppUserId == user.Id)
            .Select(p => new Stock {
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

    public async Task<bool> ContainsStockAsync(AppUser user, Stock stock) {
        return await _context.Portfolios.AnyAsync(p => p.AppUserId == user.Id && p.StockId == stock.Id);
    }

    public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio) {
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }

    public async Task<Portfolio?> DeletePortfolioAsync(AppUser user, Stock stock) {
        var portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.AppUserId == user.Id && p.StockId == stock.Id);

        if (portfolio is null) {
            return null;
        }
        
        _context.Portfolios.Remove(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }
}
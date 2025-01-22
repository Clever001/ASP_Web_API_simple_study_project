using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Dtos.Stock;
using WebApplication3.Helpers;
using WebApplication3.Interfaces;
using WebApplication3.Mappers;

namespace WebApplication3.Repository;

public class StockRepository : IStockRepository {
    private ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context) {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync(StockQuery query) {
        var stocks = _context.Stocks.Include(st => st.Comments).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Symbol)) {
            var upperInvariant = query.Symbol.ToUpper();
            stocks = stocks.Where(st => st.Symbol.ToUpper().Contains(upperInvariant));
        }

        if (!string.IsNullOrWhiteSpace(query.CompanyName)) {
            var upperInvariant = query.CompanyName.ToUpper();
            stocks = stocks.Where(st => st.CompanyName.ToUpper().Contains(upperInvariant));
        }

        if (!string.IsNullOrWhiteSpace(query.OrderBy)) {
            if (string.Equals(query.OrderBy, "symbol", StringComparison.InvariantCultureIgnoreCase)) {
                stocks = query.IsDescending ? 
                    stocks.OrderByDescending(st => st.Symbol) : stocks.OrderBy(st => st.Symbol);
            }
            else if (string.Equals(query.OrderBy, "companyName", StringComparison.InvariantCultureIgnoreCase)) {
                stocks = query.IsDescending ? 
                    stocks.OrderByDescending(st => st.CompanyName) : stocks.OrderBy(st => st.CompanyName);
            }
        }
        
        int skipNum = (query.PageNumber - 1) * query.PageSize;
        query.PageSize = Math.Min(query.PageSize, 20);

        if (skipNum < 1) skipNum = 0;
        if (query.PageSize < 1) query.PageSize = 10;
        
        return await stocks.Skip(skipNum).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id) {
        return await _context.Stocks.Include(st => st.Comments).FirstOrDefaultAsync(st => st.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stock) {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto) {
        var stock = await _context.Stocks.FindAsync(id);
        if (stock is null) return null;
        stock.Update(stockDto);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> DeleteAsync(int id) {
        var stock = await _context.Stocks.FindAsync(id);
        if (stock is null) return null;
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<bool> StockExists(int id) {
        return await _context.Stocks.AnyAsync(st => st.Id == id);
    }
}
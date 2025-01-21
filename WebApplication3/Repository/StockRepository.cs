using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Dtos.Stock;
using WebApplication3.Interfaces;
using WebApplication3.Mappers;

namespace WebApplication3.Repository;

public class StockRepository : IStockRepository {
    private ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context) {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync() {
        return await _context.Stocks.Include(st => st.Comments).ToListAsync();
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
}
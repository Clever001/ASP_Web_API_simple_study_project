using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Interfaces;

namespace WebApplication3.Repository;

public class StockRepository : IStockRepository {
    private ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context) {
        _context = context;
    }
    
    public Task<List<Stock>> GetAllAsync() {
        return _context.Stocks.ToListAsync();
    }
}
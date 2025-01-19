using api.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dtos.Stock;
using WebApplication3.Interfaces;
using WebApplication3.Mappers;

namespace WebApplication3.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase {
    private ApplicationDbContext _context;
    private IStockRepository _stockRepo;

    public StockController(ApplicationDbContext context, IStockRepository stockRepository) {
        _context = context;
        _stockRepo = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var stocks = await _stockRepo.GetAllAsync();
        return Ok(stocks.Select(st => st.ToStockDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id) {
        var stock = await _context.Stocks.FindAsync(id);
        if (stock is null) {
            return NotFound();
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto) {
        var stock = createStockDto.ToStockFromCreateDto();
        await _context.AddAsync(stock);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto) {
        var stock = await _context.Stocks.FindAsync(id);
        if (stock is null) {
            return NotFound();
        }

        stock.Update(updateStockDto);
        await _context.SaveChangesAsync();
        return Ok(stock.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        var stock = await _context.Stocks.FindAsync(id);
        if (stock is null) {
            return NotFound();
        }

        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
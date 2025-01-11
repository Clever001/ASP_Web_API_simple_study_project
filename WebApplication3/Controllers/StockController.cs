using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Dtos.Stock;
using WebApplication3.Mappers;

namespace WebApplication3.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase {
    private ApplicationDbContext _context;

    public StockController(ApplicationDbContext context) {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() {
        return Ok(_context.Stocks
                                 .ToList()
                                 .Select(st => st.ToStockDto()));
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id) {
        var stock = _context.Stocks.Find(id);
        if (stock is null) {
            return NotFound();
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockDto createStockDto) {
        var stock = createStockDto.ToStockFromCreateDto();
        _context.Add(stock);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto) {
        var stock = _context.Stocks.Find(id);
        if (stock is null) {
            return NotFound();
        }

        stock.Update(updateStockDto);
        _context.SaveChanges();
        return Ok(stock.ToStockDto());
    }
}
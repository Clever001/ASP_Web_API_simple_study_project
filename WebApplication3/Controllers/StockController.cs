using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        return Ok(_context.Stocks.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id) {
        var stock = _context.Stocks.Find(id);
        if (stock is null) {
            return NotFound();
        }

        return Ok(stock);
    }
}
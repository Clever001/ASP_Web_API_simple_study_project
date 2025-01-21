using api.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dtos.Stock;
using WebApplication3.Interfaces;
using WebApplication3.Mappers;

namespace WebApplication3.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase {
    private IStockRepository _stockRepo;

    public StockController(IStockRepository stockRepository) {
        _stockRepo = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var stocks = await _stockRepo.GetAllAsync();
        return Ok(stocks.Select(st => st.ToStockDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id) {
        var stock = await _stockRepo.GetByIdAsync(id);
        if (stock is null) {
            return NotFound();
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto) {
        var stock = await _stockRepo.CreateAsync(createStockDto.ToStockFromCreateDto());
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto) {
        var stock = await _stockRepo.UpdateAsync(id, updateStockDto);
        if (stock is null) {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        if (await _stockRepo.DeleteAsync(id) is not null) {
            return NoContent();
        }
        return NotFound();
    }
}
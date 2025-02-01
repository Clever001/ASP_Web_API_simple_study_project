using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Extensions;
using WebApplication3.Interfaces;
using WebApplication3.Mappers;

namespace WebApplication3.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase {
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    
    public PortfolioController(UserManager<AppUser> userManager,
        IStockRepository stockRepository,
        IPortfolioRepository portfolioRepository) {
        _userManager = userManager;
        _stockRepository = stockRepository;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio() {
        var userName = User.GetUserName();
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) return BadRequest();
        var stocks = await _portfolioRepository.GetStocksAsync(user);
        return Ok(stocks.Select(st => st.ToStockDto()).ToList());
    }

    [HttpPost("{symbol}")]
    [Authorize]
    public async Task<IActionResult> AddPortfolio([FromRoute] string symbol) {
        var userName = User.GetUserName();
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) return BadRequest();
        var stock = await _stockRepository.GetBySymbolAsync(symbol);
        if (stock is null) return NotFound("Stock not found");
        
        if (await _portfolioRepository.ContainsStockAsync(user, stock))
            return BadRequest("Stock already contains in portfolio");
        
        var portfolio = new Portfolio {
            AppUserId = user.Id,
            StockId = stock.Id,
        };
        portfolio = await _portfolioRepository.CreatePortfolioAsync(portfolio);

        if (portfolio is null) {
            return StatusCode(500, "Portfolio could not be created");
        }

        return Created();
    }

    [HttpDelete("{symbol}")]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio([FromRoute] string symbol) {
        var userName = User.GetUserName();
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) return BadRequest();
        
        var stocks = await _portfolioRepository.GetStocksAsync(user);
        
        var filteredStock = stocks.SingleOrDefault(st => st.Symbol == symbol);
        
        if (filteredStock is null) 
            return NotFound("Stock is not in your portfolio");
        
        await _portfolioRepository.DeletePortfolioAsync(user, filteredStock);
        return Ok();
    }
}
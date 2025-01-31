using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Extensions;
using WebApplication3.Interfaces;

namespace WebApplication3.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase {
    private readonly UserManager<AppUser> _userManager;
    private readonly IPortfolioRepository _portfolioRepository;
    
    public PortfolioController(UserManager<AppUser> userManager, 
        IPortfolioRepository portfolioRepository) {
        _userManager = userManager;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio() {
        var userName = User.GetUserName();
        var user = await _userManager.FindByNameAsync(userName);
        var stocks = await _portfolioRepository.GetStocksAsync(user);
        return Ok(stocks);
    }
}
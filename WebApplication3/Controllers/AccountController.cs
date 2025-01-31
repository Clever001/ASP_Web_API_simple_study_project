using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dtos.Account;
using WebApplication3.Interfaces;

namespace WebApplication3.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase {
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        
        if (user is null) return Unauthorized("Invalid username or password");
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) {
            return Unauthorized("Invalid username or password");
        }

        return Ok(
            new NewUserDto {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
            }
        );
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) {
        try {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var appUser = new AppUser {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };
            
            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (createdUser.Succeeded) {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded) {
                    var newUser = new NewUserDto {
                        Email = appUser.Email,
                        UserName = appUser.UserName,
                        Token = _tokenService.CreateToken(appUser),
                    };
                    return Ok(newUser);
                }
                
                return StatusCode(500, roleResult.Errors);
            }
            
            return StatusCode(500, createdUser.Errors);
        }
        catch (Exception e) {
            return StatusCode(500, e);
        }
    }
}
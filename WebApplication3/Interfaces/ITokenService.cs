using api.Models;

namespace WebApplication3.Interfaces;

public interface ITokenService {
    string CreateToken(AppUser user);
}
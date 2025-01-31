using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class AppUser : IdentityUser {
    // At start everything will be default!
    public List<Portfolio> Portfolios { get; set; } = new();
}
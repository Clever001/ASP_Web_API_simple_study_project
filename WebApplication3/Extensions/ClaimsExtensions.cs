using System.Security.Claims;

namespace WebApplication3.Extensions;

public static class ClaimsExtensions {
    public static string GetUserName(this ClaimsPrincipal principal) {
        return principal.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.GivenName))?.Value ?? string.Empty;
    }
}
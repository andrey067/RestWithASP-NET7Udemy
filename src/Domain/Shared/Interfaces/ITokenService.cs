using System.Security.Claims;

namespace Domain.Shared.Interfaces
{
    public interface ITokenService
    {       
        string GenerateAccessToken(IEnumerable<Claim> claimsIdentity);
        string GenerateRefreshToken();
       ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

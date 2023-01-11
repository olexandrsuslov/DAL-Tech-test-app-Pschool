using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PschoolAPIback.Context;

namespace PschoolAPIback.TokenHelpers;

public interface ITokenService
{
    SigningCredentials GetSigningCredentials();
    Task<List<Claim>> GetClaims(User user);
    JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
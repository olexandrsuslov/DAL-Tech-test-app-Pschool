using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pschool.Models.Dtos;
using PschoolAPIback.Context;
using PschoolAPIback.TokenHelpers;

namespace PschoolAPIback.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    public TokenController(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh([FromBody]RefreshTokenDto tokenDto)
    {
        if (tokenDto is null)
        {
            Console.WriteLine("content is null");
            return BadRequest(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = "Invalid client request" });
        }
        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenDto.Token);
        var username = principal.Identity.Name;
        var user = await _userManager.FindByEmailAsync(username);
        if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = "Invalid client request" });
        var signingCredentials = _tokenService.GetSigningCredentials();
        var claims = await _tokenService.GetClaims(user);
        var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        user.RefreshToken = _tokenService.GenerateRefreshToken();
        await _userManager.UpdateAsync(user);
        return Ok(new AuthResponseDto { Token = token, RefreshToken = user.RefreshToken, IsAuthSuccessful = true });
    }
}
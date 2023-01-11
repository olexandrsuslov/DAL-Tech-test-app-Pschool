using Microsoft.AspNetCore.Identity;

namespace PschoolAPIback.Context;

public class User : IdentityUser
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
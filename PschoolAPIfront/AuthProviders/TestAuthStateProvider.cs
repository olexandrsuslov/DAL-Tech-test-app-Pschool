using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace PschoolAPIfront.AuthProviders;

public class TestAuthStateProvider : AuthenticationStateProvider
{
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var anonymous = new ClaimsIdentity();
        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
    }
}
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Pschool.Models.Dtos;
using PschoolAPIfront.AuthProviders;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;
    private readonly AuthenticationStateProvider _authStateProvider; 
    private readonly ILocalStorageService _localStorage;
    public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
    {
        _client = client;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _authStateProvider = authStateProvider; 
        _localStorage = localStorage;
    }
    public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        var content = JsonSerializer.Serialize(userForRegistration);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var registrationResult = await _client.PostAsync("api/accounts/Registration", bodyContent);
        var registrationContent = await registrationResult.Content.ReadAsStringAsync();
        if (!registrationResult.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, _options);
            return result;
        }
        return new RegistrationResponseDto { IsSuccessfulRegistration = true };
    }
    
    public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
    {
        var content = JsonSerializer.Serialize(userForAuthentication);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var authResult = await _client.PostAsync("api/accounts/Login", bodyContent);
        var authContent = await authResult.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<AuthResponseDto>(authContent, _options);
        if (!authResult.IsSuccessStatusCode)
            return result;
        
        await _localStorage.SetItemAsync("authToken", result.Token);
        await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
        return new AuthResponseDto { IsAuthSuccessful = true };
    }
    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("refreshToken");
        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _client.DefaultRequestHeaders.Authorization = null;
    }
    
    public async Task<string> RefreshToken()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
        var tokenDto = JsonSerializer.Serialize(new RefreshTokenDto { Token = token, RefreshToken = refreshToken });
        Console.WriteLine(tokenDto);
        var bodyContent = new StringContent(tokenDto, Encoding.UTF8, "application/json");
        var refreshResult = await _client.PostAsync("api/token/Refresh", bodyContent);
        var refreshContent = await refreshResult.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<AuthResponseDto>(refreshContent, _options);
        if (!refreshResult.IsSuccessStatusCode)
            throw new ApplicationException(refreshContent);
        await _localStorage.SetItemAsync("authToken", result.Token);
        await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
            
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
        return result.Token;
    }
    
}

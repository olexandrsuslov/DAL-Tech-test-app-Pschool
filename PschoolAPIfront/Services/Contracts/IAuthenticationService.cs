using Pschool.Models.Dtos;

namespace PschoolAPIfront.Services.Contracts;

public interface IAuthenticationService
{
    Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);
    Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication);
    Task Logout();
    Task<string> RefreshToken();
}
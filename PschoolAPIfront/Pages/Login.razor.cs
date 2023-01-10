using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Pages;

public partial class Login
{
    private UserForAuthenticationDto _userForAuthentication = new UserForAuthenticationDto();
    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public bool ShowAuthError { get; set; }
    public string Error { get; set; }
    public async Task ExecuteLogin()
    {
        ShowAuthError = false;
        var result = await AuthenticationService.Login(_userForAuthentication);
        if (!result.IsAuthSuccessful)
        {
            Error = result.ErrorMessage;
            ShowAuthError = true;
        }
        else
        {
            NavigationManager.NavigateTo("/");
        }
    }
}

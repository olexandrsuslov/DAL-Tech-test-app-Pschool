using Microsoft.AspNetCore.Components;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Pages;

public partial class Logout
{
    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await AuthenticationService.Logout();
        NavigationManager.NavigateTo("/");
    }
}
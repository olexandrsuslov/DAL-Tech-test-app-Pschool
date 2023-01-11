using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PschoolAPIfront;
using PschoolAPIfront.AuthProviders;
using PschoolAPIfront.HttpRepository;
using PschoolAPIfront.Pages;
using PschoolAPIfront.Services;
using PschoolAPIfront.Services.Contracts;
using Syncfusion.Blazor;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7269/") }.EnableIntercept(sp));



builder.Services.AddScoped<IParentService, ParentService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddHttpClientInterceptor();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<HttpInterceptorService>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddMudServices();
await builder.Build().RunAsync();
using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.HttpRepository;
using PschoolAPIfront.Services.Contracts;
using PschoolAPIfront.Shared;

namespace PschoolAPIfront.Pages;

public class AddParentBase : ComponentBase,IDisposable
{
    public ParentDto parent { get; set; }
    [Inject]
    public IParentService parentService { get; set; }

    [Inject]
    public HttpInterceptorService Interceptor { get; set; }
    public string ErrorMessage { get; set; }

    public SuccessNotification _notification;

    protected async override Task OnInitializedAsync()
    {
        parent = new ParentDto();
    }
    public async Task HandleValidSubmit()
    {
        Interceptor.RegisterEvent(); 
        try
    {
        parent = await parentService.AddItem(parent);
        Console.WriteLine(parent);
        _notification.Show();
    }
    catch (Exception e)
    {
        Console.WriteLine(" - error in handle method");
        ErrorMessage = e.Message;
    }
    }
    
    public void Dispose() => Interceptor.DisposeEvent();
    
}
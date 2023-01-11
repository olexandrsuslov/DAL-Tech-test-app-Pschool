using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.HttpRepository;
using PschoolAPIfront.Services;
using PschoolAPIfront.Services.Contracts;
using PschoolAPIfront.Shared;

namespace PschoolAPIfront.Pages;

public class EditParentBase : ComponentBase, IDisposable
{
    [Parameter]
    public int Id { get; set; }
    public ParentDto parent { get; set; }
    [Inject]
    public IParentService parentService { get; set; }
    
    [Inject]
    public HttpInterceptorService Interceptor { get; set; }

    public string ErrorMessage { get; set; }
    
    public SuccessNotification _notification;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Interceptor.RegisterEvent();
            parent = await parentService.GetItem(Id);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }
    public async Task HandleValidSubmit()
    {
        try
        {
            var response = await parentService.EditItem(parent);
            Console.WriteLine(response);
            _notification.Show();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ErrorMessage = e.Message;
        }
    }
    
    public void Dispose() => Interceptor.DisposeEvent();
}
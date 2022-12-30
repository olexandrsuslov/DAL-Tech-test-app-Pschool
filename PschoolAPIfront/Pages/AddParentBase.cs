using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services.Contracts;
using PschoolAPIfront.Shared;

namespace PschoolAPIfront.Pages;

public class AddParentBase : ComponentBase
{
    public ParentDto parent { get; set; }
    [Inject]
    public IParentService parentService { get; set; }

    public string ErrorMessage { get; set; }

    public SuccessNotification _notification;

    protected override void OnInitialized()
    {
        parent = new ParentDto();
    }
    public async Task HandleValidSubmit()
    {
        try
    {
        parent = await parentService.AddItem(parent);
        Console.WriteLine(parent);
        _notification.Show();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        ErrorMessage = e.Message;
    }
    }
    
}
using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Pages;

public class AddParentBase : ComponentBase
{
    public ParentDto parent { get; set; }
    [Inject]
    public IParentService parentService { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsValid;

    protected override void OnInitialized()
    {
        parent = new ParentDto();
    }
    public async Task HandleValidSubmit()
    {
        IsValid = true;
    try
    {
        parent = await parentService.AddItem(parent);
        Console.WriteLine(parent);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        ErrorMessage = e.Message;
    }
    IsValid = false;
    }
    
}
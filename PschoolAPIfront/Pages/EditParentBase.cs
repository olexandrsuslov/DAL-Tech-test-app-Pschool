using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Pages;

public class EditParentBase : ComponentBase
{
    [Parameter]
    public int Id { get; set; }
    public ParentDto parent { get; set; }
    [Inject]
    public IParentService parentService { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsValid;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            parent = await parentService.GetItem(Id);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }
    public async Task HandleValidSubmit()
    {
        IsValid = true;
        try
        {
            var response = await parentService.EditItem(parent);
            Console.WriteLine(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ErrorMessage = e.Message;
        }
        IsValid = false;
    }
}
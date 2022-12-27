using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Pages;

public class EditStudentBase : ComponentBase
{
    [Parameter]
    public int Id { get; set; }
    public StudentDto student { get; set; }
    [Inject]
    public IStudentService studentService { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsValid;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            student = await studentService.GetItem(Id);
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
            var response = await studentService.EditItem(student);
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
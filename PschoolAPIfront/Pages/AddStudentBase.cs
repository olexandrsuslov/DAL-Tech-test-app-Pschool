using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Pages;

public class AddStudentBase : ComponentBase
{
    public StudentDto student { get; set; }
    [Inject]
    public IStudentService studentService { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsValid;

    protected override void OnInitialized()
    {
        student = new StudentDto();
    }
    public async Task HandleValidSubmit()
    {
        IsValid = true;
        try
        {
            student = await studentService.AddItem(student);
            Console.WriteLine(student);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ErrorMessage = e.Message;
        }
        IsValid = false;
    }
    
}
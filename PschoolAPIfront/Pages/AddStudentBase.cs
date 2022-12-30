using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services.Contracts;
using PschoolAPIfront.Shared;

namespace PschoolAPIfront.Pages;

public class AddStudentBase : ComponentBase
{
    public StudentDto student { get; set; }
    [Inject]
    public IStudentService studentService { get; set; }
    
    public SuccessNotification _notification;

    public string ErrorMessage { get; set; }
    
    protected override void OnInitialized()
    {
        student = new StudentDto();
    }
    public async Task HandleValidSubmit()
    {
        try
        {
            student = await studentService.AddItem(student);
            Console.WriteLine(student);
            _notification.Show();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ErrorMessage = e.Message;
        }
    }
    
}
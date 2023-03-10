using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.HttpRepository;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Pages;

public class StudentsBase : ComponentBase
{
    [Inject]
    public IStudentService StudentService { get; set; }
    [Inject]
    public IParentService ParentService { get; set; }
    
    public IEnumerable<ParentDto> Parents { get; set; } 

    protected override async Task OnInitializedAsync()
    {
        Parents = await ParentService.GetItemsDefault();
    }

}
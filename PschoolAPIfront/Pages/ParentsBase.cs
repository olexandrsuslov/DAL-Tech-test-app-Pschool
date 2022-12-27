using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Pages;

public class ParentsBase : ComponentBase
{
    [Inject]
    public IParentService ParentService { get; set; }

    public IEnumerable<ParentDto> Parents { get; set; }

    protected override async  Task OnInitializedAsync()
    {
        Parents = await ParentService.GetItems();
    }
}
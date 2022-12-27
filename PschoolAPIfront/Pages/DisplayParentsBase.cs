using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Pages;

public class DisplayParentsBase : ComponentBase
{
    [Parameter]
    public List<ParentDto> Parents { get; set; }
    
    [Inject]
    public IParentService ParentService { get; set; }
    
    protected async Task DeleteItem_Click(int id)
    {
        var response = await ParentService.DeleteItem(id);
        RemoveItem(id);

    }
    
    private void RemoveItem(int id)
    { 
        var parentDto = GetItem(id);

        Parents.Remove(parentDto);
    }
    
    private ParentDto GetItem(int id)
    {
        return Parents.FirstOrDefault(i => i.ParentId == id);
    }
    
    
    
}
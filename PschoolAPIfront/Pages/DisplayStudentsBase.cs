using Microsoft.AspNetCore.Components;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services.Contracts;
using Syncfusion.Blazor.DropDowns;

namespace PschoolAPIfront.Pages;

public class DisplayStudentsBase : ComponentBase
{
    public class ParentDropDown
    {  
        public int Id { get; set; }
        public string Name { get; set; }
    }
    [Parameter]
    public List<StudentDto> Students { get; set; }
    public List<StudentDto> StudentsDefaut { get; set; }
    
    [Inject]
    public IStudentService StudentService { get; set; }
    [Parameter]
    public List<ParentDto> Parents { get; set; }

    public List<ParentDropDown> parentsdropdown;

    [Inject]
    public IParentService ParentService { get; set; }

    public ParentDto parentSelected = new();

    protected async Task DeleteItem_Click(int id)
    {
        var response = await StudentService.DeleteItem(id);
        RemoveItem(id);

    }

    protected override void OnInitialized()
    {
        parentsdropdown = ConvertToParentDropDownList();
        StudentsDefaut = Students;
    }

    private void RemoveItem(int id)
    { 
        var studentDto = GetItem(id);

        Students.Remove(studentDto);
    }
    
    private StudentDto GetItem(int id)
    {
        return Students.FirstOrDefault(i => i.Id == id);
    }

    private List<ParentDropDown> ConvertToParentDropDownList()
    {
        return (from parent in Parents
            select new ParentDropDown
            {
                Id = parent.ParentId,
                Name = parent.FirstName
                
            }).ToList();
    }
    public void OnValueChange(ChangeEventArgs<int, ParentDropDown> args)
    {
        Console.WriteLine("The DropDownList Value is:{0} ", args.Value);
        Students = StudentsDefaut.Where(x => x.ParentId == args.Value).ToList();

    }
}
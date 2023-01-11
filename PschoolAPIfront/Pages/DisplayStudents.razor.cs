using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pschool.Models.Dtos;
using Pschool.Models.RequestFeatures;
using PschoolAPIfront.HttpRepository;
using PschoolAPIfront.Services.Contracts;
using Syncfusion.Blazor.DropDowns;

namespace PschoolAPIfront.Pages;

public partial class DisplayStudents
{
    public class ParentDropDown
    {  
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public MudTable<StudentDto> _table;
    private DisplayParameters _displayParameters = new DisplayParameters();
    public readonly int[] _pageSizeOption = { 4, 6, 8 };

    // [Inject]
    // public HttpInterceptorService Interceptor { get; set; }
    [Inject]
    public IStudentService StudentService { get; set; }
    [Parameter]
    public List<ParentDto> Parents { get; set; }

    public List<ParentDropDown> parentsdropdown;

    [Inject]
    public IParentService ParentService { get; set; }
    
    // protected async override Task OnInitializedAsync()
    // {
    //     Interceptor.RegisterEvent();
    // }

    protected async Task DeleteItem_Click(int id)
    {
        var response = await StudentService.DeleteItem(id);
        _table.ReloadServerData();

    }

    protected override void OnInitialized()
    {
        parentsdropdown = ConvertToParentDropDownList();
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
        _displayParameters.SearchParent = args.Value.ToString();
        _table.ReloadServerData();
    }
    
    public async Task<TableData<StudentDto>> GetServerData(TableState state)
    {
        _displayParameters.PageSize = state.PageSize;
        _displayParameters.PageNumber = state.Page + 1;
        _displayParameters.OrderBy = state.SortDirection == SortDirection.Descending ?
            state.SortLabel + " desc" :
            state.SortLabel;
        var response = await StudentService.GetItems(_displayParameters);
        return new TableData<StudentDto>
        {
            Items = response.Items,
            TotalItems = response.MetaData.TotalCount
        };
    }

    public void ChangeDefault()
    {
        _displayParameters.SearchParent = null;
        _table.ReloadServerData();
    }
    private void OnSearch(string searchTerm)
    {
        _displayParameters.SearchTerm = searchTerm;
        _table.ReloadServerData();
    }
    
    // public void Dispose() => Interceptor.DisposeEvent();
}
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pschool.Models.Dtos;
using Pschool.Models.RequestFeatures;
using PschoolAPIfront.HttpRepository;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Pages;

public partial class DisplayParents
{
    public MudTable<ParentDto> _table;
    private DisplayParameters _displayParameters = new DisplayParameters();
    public readonly int[] _pageSizeOption = { 4, 6, 8 };
    
    // [Inject]
    // public HttpInterceptorService Interceptor { get; set; }
    
    [Inject]
    public IParentService ParentService { get; set; }
    
    //
    // protected async override Task OnInitializedAsync()
    // {
    //     Interceptor.RegisterEvent();
    // }
    protected async Task DeleteItem_Click(int id)
    {
        
        var response = await ParentService.DeleteItem(id);
        _table.ReloadServerData();
        // RemoveItem(id);

    }
    public async Task<TableData<ParentDto>> GetServerData(TableState state)
    {
        _displayParameters.PageSize = state.PageSize;
        _displayParameters.PageNumber = state.Page + 1;
        _displayParameters.OrderBy = state.SortDirection == SortDirection.Descending ?
            state.SortLabel + " desc" :
            state.SortLabel;
        var response = await ParentService.GetItems(_displayParameters);
        return new TableData<ParentDto>
        {
            Items = response.Items,
            TotalItems = response.MetaData.TotalCount
        };
    }
    
    private void OnSearch(string searchTerm)
    {
        _displayParameters.SearchTerm = searchTerm;
        _table.ReloadServerData();
    }
    
    // public void Dispose() => Interceptor.DisposeEvent();

}
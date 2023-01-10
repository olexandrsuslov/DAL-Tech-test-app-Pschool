using Pschool.Models.Dtos;
using Pschool.Models.RequestFeatures;
using PschoolAPIfront.Features;

namespace PschoolAPIfront.Services.Contracts;

public interface IParentService
{
    Task<PagingResponse<ParentDto>> GetItems(DisplayParameters _displayParameters);
    Task<ParentDto> GetItem(int id);
    
    Task<IEnumerable<ParentDto>> GetItemsDefault();

    Task<ParentDto> AddItem(ParentDto parentDto);
    Task<HttpResponseMessage> DeleteItem(int id);
    Task<HttpResponseMessage> EditItem(ParentDto parentDto);



}
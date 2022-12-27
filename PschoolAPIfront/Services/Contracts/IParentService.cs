using Pschool.Models.Dtos;

namespace PschoolAPIfront.Services.Contracts;

public interface IParentService
{
    Task<IEnumerable<ParentDto>> GetItems();
    Task<ParentDto> GetItem(int id);

    Task<ParentDto> AddItem(ParentDto parentDto);
    Task<HttpResponseMessage> DeleteItem(int id);
    Task<HttpResponseMessage> EditItem(ParentDto parentDto);



}
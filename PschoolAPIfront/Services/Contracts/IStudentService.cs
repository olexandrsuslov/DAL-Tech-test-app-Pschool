using Pschool.Models.Dtos;
using Pschool.Models.RequestFeatures;
using PschoolAPIfront.Features;

namespace PschoolAPIfront.Services.Contracts;

public interface IStudentService
{
 
    Task<PagingResponse<StudentDto>> GetItems(DisplayParameters _displayParameters);
    Task<StudentDto> GetItem(int id);

    Task<StudentDto> AddItem(StudentDto studentDto);
    Task<HttpResponseMessage> DeleteItem(int id);
    Task<HttpResponseMessage> EditItem(StudentDto studentDto);
}
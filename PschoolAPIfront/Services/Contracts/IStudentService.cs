using Pschool.Models.Dtos;

namespace PschoolAPIfront.Services.Contracts;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetItems();
    Task<StudentDto> GetItem(int id);

    Task<StudentDto> AddItem(StudentDto studentDto);
    Task<HttpResponseMessage> DeleteItem(int id);
    Task<HttpResponseMessage> EditItem(StudentDto studentDto);
}
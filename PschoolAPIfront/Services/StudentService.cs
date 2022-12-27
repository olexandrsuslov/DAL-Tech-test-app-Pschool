using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Services;

public class StudentService : IStudentService
{
    private readonly HttpClient httpClient;

    public StudentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<StudentDto>> GetItems()
    {
        try
        { 
            var response = await this.httpClient.GetAsync("api/Student");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<StudentDto>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<StudentDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} message: {message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<StudentDto> GetItem(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"api/Student/{id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(StudentDto);
                }

                return await response.Content.ReadFromJsonAsync<StudentDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} message: {message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<StudentDto> AddItem(StudentDto studentDto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync<StudentDto>("api/Student",studentDto);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(StudentDto);
                }

                return await response.Content.ReadFromJsonAsync<StudentDto>();

            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<HttpResponseMessage> DeleteItem(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/Student/{id}");
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<HttpResponseMessage> EditItem(StudentDto studentDto)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(studentDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PutAsync($"api/Student/{studentDto.Id}", content);
            
            return response;
           

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
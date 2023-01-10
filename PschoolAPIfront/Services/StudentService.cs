using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Pschool.Models.Dtos;
using Pschool.Models.RequestFeatures;
using PschoolAPIfront.Features;
using PschoolAPIfront.Services.Contracts;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PschoolAPIfront.Services;

public class StudentService : IStudentService
{
    private readonly HttpClient httpClient;
    private readonly JsonSerializerOptions _options;

    public StudentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<PagingResponse<StudentDto>> GetItems(DisplayParameters _displayParameters)
    {
        try
        {

            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = _displayParameters.PageNumber.ToString(),
                ["pageSize"] = _displayParameters.PageSize.ToString(),
                ["searchParent"] = _displayParameters.SearchParent ?? "",
                ["searchTerm"] = _displayParameters.SearchTerm ?? "",
                ["orderBy"] = _displayParameters.OrderBy ?? "name"
            };
            using (var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("api/Student", queryStringParam)))
            {
                response.EnsureSuccessStatusCode();
                var metaData = System.Text.Json.JsonSerializer
                    .Deserialize<MetaData>(response.Headers.GetValues("Y-Paging").First(), _options);
                var stream = await response.Content.ReadAsStreamAsync();
                
                var pagingResponse = new PagingResponse<StudentDto>
                {
                    Items = await JsonSerializer.DeserializeAsync<List<StudentDto>>(stream, _options),
                    MetaData = metaData
                };
                return pagingResponse;
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
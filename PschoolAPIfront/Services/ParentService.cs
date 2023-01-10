using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Pschool.Models.Dtos;
using Pschool.Models.RequestFeatures;
using PschoolAPIfront.Services.Contracts;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;
using System.Text.Json;
using PschoolAPIfront.Features;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PschoolAPIfront.Services;

public class ParentService : IParentService
{
    private readonly HttpClient httpClient;
    private readonly JsonSerializerOptions _options;

    public ParentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<PagingResponse<ParentDto>> GetItems(DisplayParameters _displayParameters)
    {
        try
        {

            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = _displayParameters.PageNumber.ToString(),
                ["pageSize"] = _displayParameters.PageSize.ToString(),
                ["searchTerm"] = _displayParameters.SearchTerm ?? "",
                ["orderBy"] = _displayParameters.OrderBy ?? "name"
            };
            using (var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("api/Parent", queryStringParam)))
            {
                response.EnsureSuccessStatusCode();
                var metaData = JsonSerializer
                    .Deserialize<MetaData>(response.Headers.GetValues("X-Paging").First(), _options);
                var stream = await response.Content.ReadAsStreamAsync();
                
                var pagingResponse = new PagingResponse<ParentDto>
                {
                    Items = await JsonSerializer.DeserializeAsync<List<ParentDto>>(stream, _options),
                    MetaData = metaData
                };
                return pagingResponse;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.WriteLine("problem with headers");
            throw;
        }
    }

    public async Task<ParentDto> GetItem(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"api/Parent/{id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(ParentDto);
                }

                return await response.Content.ReadFromJsonAsync<ParentDto>();
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
    
    public async Task<IEnumerable<ParentDto>> GetItemsDefault()
    {
        try
        { 
            var response = await this.httpClient.GetAsync("api/Parent/GetParents");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<ParentDto>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<ParentDto>>();
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

    public async Task<ParentDto> AddItem(ParentDto parentDto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync<ParentDto>("api/Parent",parentDto);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(ParentDto);
                }

                return await response.Content.ReadFromJsonAsync<ParentDto>();

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
            var response = await httpClient.DeleteAsync($"api/Parent/{id}");
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<HttpResponseMessage> EditItem(ParentDto parentDto)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(parentDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PutAsync($"api/Parent/{parentDto.ParentId}", content);
            
            return response;
           

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
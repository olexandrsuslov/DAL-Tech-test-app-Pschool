using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Pschool.Models.Dtos;
using PschoolAPIfront.Services.Contracts;

namespace PschoolAPIfront.Services;

public class ParentService : IParentService
{
    private readonly HttpClient httpClient;

    public ParentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<ParentDto>> GetItems()
    {
        try
        { 
            var response = await this.httpClient.GetAsync("api/Parent");
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
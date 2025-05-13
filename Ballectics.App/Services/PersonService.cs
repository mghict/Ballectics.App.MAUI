using System.Net.Http.Json;
using Ballectics.App.Models;

namespace Ballectics.App.Services;

public class PersonService : BaseServices
{
    public PersonService(IHttpClientFactory httpClient, StorageService storageService) : base(httpClient, storageService)
    {
    }
    public async Task<ResultModel<List<Models.PersonModel>>> GetAllAsync(string? search, int count = -1)
    {
        var result = new ResultModel<List<Models.PersonModel>>();
        try
        {
            var query = $"count={count}";

            if (!string.IsNullOrWhiteSpace(search))
            {
                query += $"&search={Uri.EscapeDataString(search)}";
            }

            var response = await _httpClient.GetAsync($"person/list?{query}");

            result = await response.Content.ReadFromJsonAsync<ResultModel<List<Models.PersonModel>>>();
            if (response.IsSuccessStatusCode)
                result.SetSuccess();
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message);
        }

        return result;
    }

    public async Task<ResultModel<List<Models.PersonModel>>> GetAllAsync(int count = -1)
    {
        var result = new ResultModel<List<Models.PersonModel>>();
        try
        {
            var query = $"count={count}";

            var response = await _httpClient.GetAsync($"person/list?{query}");

            result = await response.Content.ReadFromJsonAsync<ResultModel<List<Models.PersonModel>>>();
            if (response.IsSuccessStatusCode)
                result.SetSuccess();
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message);
        }

        return result;
    }

    public async Task<ResultModel<List<Models.PersonModel>>> GetAllAsync(string? search)
    {
        var result = new ResultModel<List<Models.PersonModel>>();
        try
        {

            var response =
               !string.IsNullOrWhiteSpace(search) switch
               {
                   true => await _httpClient.GetAsync($"person/list?&search={Uri.EscapeDataString(search)}"),
                   _ => await _httpClient.GetAsync($"person/list")
               };

            result = await response.Content.ReadFromJsonAsync<ResultModel<List<Models.PersonModel>>>();
            if (response.IsSuccessStatusCode)
                result.SetSuccess();
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message);
        }

        return result;
    }

    public async Task<ResultModel<Models.PersonModel>> GetPersonAsync()
    {
        var result = new ResultModel<Models.PersonModel>();
        try
        {
            var response = await _httpClient.GetAsync($"person");

            result = await response.Content.ReadFromJsonAsync<ResultModel<Models.PersonModel>>();
            if (response.IsSuccessStatusCode)
                result.SetSuccess();
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message);
        }

        return result;
    }

    public async Task<ResultModel> SaveNewAsync(Models.PersonModel model)
    {
        var result = new ResultModel();
        try
        {
            var response = await _httpClient.PostAsJsonAsync("person", model);
            result = await response.Content.ReadFromJsonAsync<ResultModel>();

            if (response.IsSuccessStatusCode)
                result.SetSuccess();
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message);
        }

        return result;
    }

    public async Task<ResultModel> DeleteAsync(Models.PersonModel model)
    {
        var result = new ResultModel();
        try
        {
            var response = await _httpClient.DeleteAsync($"person/{model.Id}");
            result = await response.Content.ReadFromJsonAsync<ResultModel>();

            if (response.IsSuccessStatusCode)
                result.SetSuccess();
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message);
        }

        return result;
    }

    public async Task<ResultModel> SaveEditAsync(Models.PersonModel model)
    {
        var result = new ResultModel();
        try
        {
            var response = await _httpClient.PutAsJsonAsync("person", model);
            result = await response.Content.ReadFromJsonAsync<ResultModel>();

            if (response.IsSuccessStatusCode)
                result.SetSuccess();
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message);
        }

        return result;
    }

    public async Task<ResultModel> UploadImage(byte[] image, long id)
    {
        var result = new ResultModel();
        try
        {
            SetHeadersMultipart();
            SetToken();
            SetLanguage("de-DE");

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(id.ToString()), "Id");

            var imageContent = new ByteArrayContent(image);
            imageContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");
            content.Add(imageContent, "Image", "image.jpg");



            var response = await _httpClient.PostAsync("person/uploadfile", content);

            result = await response.Content.ReadFromJsonAsync<ResultModel>();

            if (response.IsSuccessStatusCode)
                result.SetSuccess();
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message);
        }
        return result;
    }
}
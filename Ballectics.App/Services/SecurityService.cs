using System.Net.Http.Json;
using Ballectics.App.Models;

namespace Ballectics.App.Services;

public class SecurityService : BaseServices
{


    public SecurityService(IHttpClientFactory httpClient, StorageService storage) : base(httpClient, storage)
    {

    }
    public async Task<ResultModel<UserLoginModel>> Login(string userName, string password)
    {
        var user = new
        {
            UserName = userName,
            Password = password
        };

        ResultModel<UserLoginModel> result = new ResultModel<UserLoginModel>();
        try
        {
            var response = await _httpClient.PostAsJsonAsync("security/login", user);
            result = await response.Content.ReadFromJsonAsync<ResultModel<UserLoginModel>>();

            if (response.IsSuccessStatusCode)
            {
                if (result is not null && result.Value is not null && !string.IsNullOrWhiteSpace(result.Value.Token))
                {
                    // Store the token in a secure place
                    var tokenSave = await _storageService.SaveUserAsync(result.Value);
                    var token = await _storageService.SaveTokenAsync(result.Value.Token);
                }

            }
        }
        catch (Exception ex)
        {
            result = new ResultModel<UserLoginModel>()
            {
                Error = ex.Message,
                IsSuccess = false,
                Value = new UserLoginModel()
                {
                }
            };
        }
        return result;
    }

    public async Task<bool> IsAutenticated()
    {
        var token = await _storageService.GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }

    public async Task Logout()
    {
        await Task.Run(() =>
        {
            _storageService.DeleteToken();
            _storageService.DeleteUser();
        });
    }
}
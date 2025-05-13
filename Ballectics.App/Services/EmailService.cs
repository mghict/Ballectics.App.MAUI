using System.Net.Http.Json;
using Ballectics.App.Models;

namespace Ballectics.App.Services;

public class EmailService : BaseServices
{
    public EmailService(IHttpClientFactory httpClientFactory, StorageService storageService) : base(httpClientFactory, storageService)
    {
    }

    public async Task<ResultModel> SendEmailAsync(EmailModel emailModel, CancellationToken cancellationToken)
    {
        var result = new ResultModel();
        try
        {
            var response = await _httpClient.PostAsJsonAsync("email/send", emailModel);
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
using System.Net.Http.Json;
using Ballectics.App.Models;

namespace Ballectics.App.Services;

public class ReportService : BaseServices
{
    public ReportService(IHttpClientFactory httpClientFactory, StorageService storageService) : base(httpClientFactory, storageService)
    {
    }
    public async Task<ResultModel<List<CourseHistoryWithImageModel>>> GetCourseHistoryWithImageAsync(DateTime? dateTime)
    {
        try
        {
            dateTime = dateTime ?? DateTime.Now;
            string formattedDate = dateTime.Value.ToString("yyyy-MM-ddTHH:mm:ss");

            string url = $"report/history?dateTime={Uri.EscapeDataString(formattedDate)}";
            var result = await _httpClient.GetFromJsonAsync<ResultModel<List<CourseHistoryWithImageModel>>>(url);

            return result;
        }
        catch (Exception ex)
        {
            return new ResultModel<List<CourseHistoryWithImageModel>>()
            {
                Error = ex.Message,
                IsSuccess = false
            };
        }
    }
}
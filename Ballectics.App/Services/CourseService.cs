using System.Net.Http.Json;
using Ballectics.App.Models;

namespace Ballectics.App.Services;

public class CourseService : BaseServices
{
    public CourseService(IHttpClientFactory httpClient, StorageService storageService)
        : base(httpClient, storageService)
    {
    }

    public async Task<ResultModel<Models.CourseModel>> GetCourseAsync(long id)
    {
        var result = new ResultModel<Models.CourseModel>();
        try
        {
            var response = await _httpClient.GetAsync($"course/{id}");
            result = await response.Content.ReadFromJsonAsync<ResultModel<Models.CourseModel>>();
            if (response.IsSuccessStatusCode)
                result.SetSuccess();
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message);
        }

        return result;
    }

    public async Task<ResultModel<Models.CourseShortInfoModel>> GetCourseShortInfoAsync(long id)
    {
        var result = new ResultModel<Models.CourseShortInfoModel>();
        try
        {
            var response = await _httpClient.GetAsync($"course/short/{id}");
            result = await response.Content.ReadFromJsonAsync<ResultModel<Models.CourseShortInfoModel>>();

            if (response.IsSuccessStatusCode)
            {
                result!.SetSuccess();
                result!.Value!.PersonId = id;
            }
        }
        catch (Exception ex)
        {
            result!.SetError(ex.Message);
        }

        return result!;
    }

    public async Task<ResultModel> IncreaseCourseAsync(Models.CourseAddModel courseInfo)
    {
        var result = new ResultModel();
        try
        {
            var response = await _httpClient.PostAsJsonAsync("course/increase", courseInfo);
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

    public async Task<ResultModel> DecreaseCourseAsync(CourseDecreaseModel model)
    {
        var result = new ResultModel();
        try
        {
            var response = await _httpClient.PostAsJsonAsync("course/decrease", model);
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

    public async Task<ResultModel<List<CourseHistoryModel>>> GetHistoryAsync(long id)
    {
        var result = new ResultModel<List<CourseHistoryModel>>();
        try
        {
            var response = await _httpClient.GetAsync($"course/history/{id}");
            result = await response.Content.ReadFromJsonAsync<ResultModel<List<CourseHistoryModel>>>();
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

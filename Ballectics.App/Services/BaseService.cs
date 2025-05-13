using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Ballectics.App.Models;

namespace Ballectics.App.Services;

public class BaseServices
{
    protected HttpClient _httpClient { get; private set; }
    protected readonly StorageService _storageService;

    public BaseServices(IHttpClientFactory httpClientFactory, StorageService storageService)
    {
        _storageService = storageService;

        _httpClient = httpClientFactory.CreateClient("BaseClient");
        SetHttpClient();
    }

    private void SetHttpClient()
    {
        SetHeadersJson();
    }

    protected void SetLanguage(string lang)
    {
        if (string.IsNullOrWhiteSpace(lang))
        {
            return;
        }
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue(lang));
    }

    protected void SetToken()
    {
        var userLogin = _storageService.GetUser();
        if (userLogin is null || string.IsNullOrWhiteSpace(userLogin.Token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            return;
        }
        _httpClient.DefaultRequestHeaders.Authorization = null;
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userLogin.Token);
    }

    protected void SetHeadersJson()
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        //_httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
    }

    protected void SetHeadersForm()
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");
        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
    }

    protected void SetHeadersMultipart()
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("multipart/form-data"));
        _httpClient.DefaultRequestHeaders.Add("Accept", "multipart/form-data");
        //_httpClient.DefaultRequestHeaders.Add("Content-Type", "multipart/form-data");
    }

    protected void SetHeadersText()
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));
        _httpClient.DefaultRequestHeaders.Add("Accept", "text/plain");
        _httpClient.DefaultRequestHeaders.Add("Content-Type", "text/plain");
    }

    protected void SetHeadersXml()
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/xml");
    }

    protected void SetHeadersOctetStream()
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/octet-stream"));
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/octet-stream");
        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/octet-stream");
    }



}

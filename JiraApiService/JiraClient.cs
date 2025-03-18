using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using JiraApi.Exceptions;
using JiraApi.JsonConverter;
using Serilog;

namespace JiraApi;

internal sealed class JiraClient : IJiraClient
{
    internal HttpClient HttpClient;
    private int _retry;
    internal int MaxRetries = 5;
    internal int RetryDelayInSeconds = 30;

    public JiraClient(string baseUrl, string username, string password)
    {
        HttpClient = new HttpClient();
        HttpClient.BaseAddress = new Uri(baseUrl);
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    public async Task<T?> Get<T>(string path) where T : class
    {
        return await ExecuteRequest<T>(() => HttpClient.GetAsync(path), path);
    }

    public async Task<T?> Post<T>(string path, object body) where T : class
    {
        return await ExecuteRequest<T>(() => HttpClient.PostAsJsonAsync(path, body, JsonOptions.Default), path);
    }

    private async Task<T?> ExecuteRequest<T>(Func<Task<HttpResponseMessage>> httpRequest, string path) where T : class
    {
        HttpResponseMessage? response = null;
        try
        {
            response = await httpRequest();
            response.EnsureSuccessStatusCode();
            Log.Debug("{Method} {Path} {Status}", response.RequestMessage?.Method, response.RequestMessage?.RequestUri,
                response.StatusCode);
            _retry = 0;
            return await ConvertResponse<T>(response);
        }
        catch (Exception e)
        {
            Log.Error("GET {BasePath}{Path} {Status} -> {Reason}", HttpClient.BaseAddress, path, "Failed", e.Message);
            if (e is HttpRequestException { StatusCode: HttpStatusCode.TooManyRequests })
            {
                _retry++;
                if (_retry > MaxRetries) throw;
                Log.Information("Retry in {sec} seconds", RetryDelayInSeconds * _retry);
                Thread.Sleep(TimeSpan.FromSeconds(RetryDelayInSeconds * _retry));
                return await ExecuteRequest<T>(httpRequest, path);
            }

            if (response?.RequestMessage?.Content != null)
                Log.Debug(await response.RequestMessage.Content.ReadAsStringAsync());
        }

        return null;
    }

    private static async Task<T> ConvertResponse<T>(HttpResponseMessage response)
    {
        try
        {
            var data = await response.Content.ReadFromJsonAsync<T>(options: JsonOptions.Default);
            if (data == null) throw new NoDataException();
            return data;
        }
        catch (Exception e)
        {
            Log.Warning("Deserialization Failed {Reason}", e.Message);
            if (response == null) throw;
            Log.Debug(response.Content.ReadAsStringAsync().Result);
            throw;
        }
    }
}

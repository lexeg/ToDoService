using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToDoService.DesktopClient.Models;

namespace ToDoService.DesktopClient.Common;

public class ToDoHttpClient : IDisposable
{
    private readonly string _uri;
    private readonly HttpClient _httpClient;

    public ToDoHttpClient(string uri)
    {
        _uri = uri;
        _httpClient = new HttpClient();
    }

    public Task<List<ToDoTask>> GetTasks()
    {
        throw new NotImplementedException();
    }

    public Task<ToDoTask> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Create(ToDoTask task)
    {
        var jsonContent = JsonConvert.SerializeObject(task);
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/tasks");
        request.Content = new StringContent(jsonContent, null, "application/json");
        var response = await _httpClient.SendAsync(request);
        return response.StatusCode == HttpStatusCode.OK;
    }

    public Task<bool> Update(int id, UpdateToDoTask task)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
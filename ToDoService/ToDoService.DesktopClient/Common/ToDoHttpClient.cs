using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using ToDoService.Client.Models;

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

    public async Task<bool> UploadFile(string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/tasks/file");
        request.Headers.Add("accept", "*/*");
        var content = new MultipartFormDataContent();
        content.Add(new StreamContent(File.OpenRead(filePath)), "formFile", fileName);
        request.Content = content;
        var response = await client.SendAsync(request);
        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> UploadBinaryFile(string fileName)
    {
        /*var bytes = await File.ReadAllBytesAsync(fileName);
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/tasks/binary-file");
        var content = new ByteArrayContent(bytes);
        content.Headers.Add("uploadedFileName", HttpUtility.UrlEncode(Path.GetFileName(fileName)));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        request.Content = content;
        var response = await _httpClient.SendAsync(request);
        return response.StatusCode == HttpStatusCode.OK;*/
        var bytes = await File.ReadAllBytesAsync(fileName);
        var content = new ByteArrayContent(bytes);
        content.Headers.Add("uploadedFileName", HttpUtility.UrlEncode(Path.GetFileName(fileName)));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        var response = await _httpClient.PostAsync($"{_uri}/tasks/binary-file", content);
        return response.StatusCode == HttpStatusCode.OK;
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
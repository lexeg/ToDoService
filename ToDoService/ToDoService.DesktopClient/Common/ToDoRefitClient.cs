using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Refit;
using ToDoService.Client;
using ToDoService.Client.Helpers;
using ToDoService.Client.Models;

namespace ToDoService.DesktopClient.Common;

public class ToDoRefitClient
{
    private readonly ITasksApi _tasksApiClient;

    public ToDoRefitClient(string uri)
    {
        _tasksApiClient = RefitHelpers.CreateTasksClient(uri);
    }

    public Task<List<ToDoTask>> GetTasks() => _tasksApiClient.GetTasks();

    public Task<ToDoTask> GetById(int id) => _tasksApiClient.GetById(id);

    public Task Create(ToDoTask task) => _tasksApiClient.CreateTask(task);

    public Task Update(int id, UpdateToDoTask task) => _tasksApiClient.Update(id, task);

    public Task Delete(int id) => _tasksApiClient.Delete(id);

    public async Task<bool> UploadFile(string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        var stream = File.OpenRead(filePath);
        var streamPart = new StreamPart(stream, fileName);
        await _tasksApiClient.UploadFile(streamPart);
        return true;
    }
}
using Refit;
using ToDoService.Client.Models;

namespace ToDoService.Client;

public interface ITasksApi
{
    [Get("/tasks")]
    Task<List<ToDoTask>> GetTasks();

    [Get("/tasks/{id}")]
    Task<ToDoTask> GetById(int id);

    [Post("/tasks")]
    Task CreateTask([Body] ToDoTask task);

    [Put("/tasks/{id}")]
    Task Update(int id, [Body] UpdateToDoTask task);

    [Delete("/tasks/{id}")]
    Task Delete(int id);

    [Multipart]
    [Post("/tasks/file")]
    Task UploadFile([AliasAs("formFile")] StreamPart stream);
}
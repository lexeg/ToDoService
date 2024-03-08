using ToDoService.Models;

namespace ToDoService.Services;

public interface ITasksService
{
    Task<List<ToDoTask>> GetTasks();
    Task<ToDoTask> GetTaskById(int id);
    Task Create(ToDoTask task);
    Task Update(ToDoTask task); // id, minimal information for update
    Task Delete(int id);
}
using ToDoService.Models;

namespace ToDoService.Services;

public class TasksService : ITasksService
{
    private readonly List<ToDoTask> _tasks;

    public TasksService()
    {
        _tasks = CreateFakeTasks();
    }

    public Task<List<ToDoTask>> GetTasks()
    {
        return Task.FromResult(_tasks);
    }

    public Task<ToDoTask> GetById(int id)
    {
        var task = _tasks.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(task);
    }

    public Task Create(ToDoTask task)
    {
        _tasks.Add(task);
        return Task.CompletedTask;
    }

    public async Task Update(int id, UpdateToDoTask task)
    {
        var foundTask = await GetById(id);
        if (foundTask == null) return;
        foundTask.Description = task.Description;
        foundTask.IsCompleted = task.IsCompleted;
    }

    public Task Delete(int id)
    {
        var task = _tasks.SingleOrDefault(x => x.Id == id);
        if (task == null) return Task.CompletedTask;
        _tasks.Remove(task);
        return Task.CompletedTask;
    }

    private static List<ToDoTask> CreateFakeTasks()
    {
        return new List<ToDoTask>
        {
            new()
            {
                Id = 1,
                Name = "Задача 1",
                Description = "",
                CreatedDate = DateTime.Now,
                DeadlineDate = DateTime.Now.AddDays(2),
                IsCompleted = false
            },
            new()
            {
                Id = 2,
                Name = "Задача 2",
                Description = "",
                CreatedDate = DateTime.Now,
                DeadlineDate = DateTime.Now.AddDays(2),
                IsCompleted = false
            },
            new()
            {
                Id = 3,
                Name = "Задача 3",
                Description = "",
                CreatedDate = DateTime.Now,
                DeadlineDate = DateTime.Now.AddDays(2),
                IsCompleted = false
            },
            new()
            {
                Id = 4,
                Name = "Задача 4",
                Description = "",
                CreatedDate = DateTime.Now,
                DeadlineDate = DateTime.Now.AddDays(2),
                IsCompleted = false
            }
        };
    }
}
using ToDoService.DataAccess.Entities;
using ToDoService.DataAccess.Repositories;
using ToDoService.Models;

namespace ToDoService.Services;

public class TasksService : ITasksService
{
    private readonly ITasksRepository _tasksRepository;

    public TasksService(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<List<ToDoTask>> GetTasks()
    {
        var tasks = (await _tasksRepository.GetTasks()).Select(ConvertToToDoTask).ToList();
        return tasks;
    }

    public async Task<ToDoTask> GetById(int id)
    {
        var entity = await _tasksRepository.GetById(id);
        return ConvertToToDoTask(entity);
    }

    public Task Create(ToDoTask task) => _tasksRepository.Create(ConvertToToDoTaskEntity(task));

    public Task Update(int id, UpdateToDoTask task) =>
        _tasksRepository.Update(id, task.Description, task.IsCompleted);

    public Task Delete(int id) => _tasksRepository.Delete(id);

    private static ToDoTask ConvertToToDoTask(ToDoTaskEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description,
        CreatedDate = entity.CreatedDate,
        DeadlineDate = entity.DeadlineDate,
        IsCompleted = entity.IsCompleted
    };

    private static ToDoTaskEntity ConvertToToDoTaskEntity(ToDoTask task) => new()
    {
        Id = task.Id,
        Name = task.Name,
        Description = task.Description,
        CreatedDate = task.CreatedDate,
        DeadlineDate = task.DeadlineDate,
        IsCompleted = task.IsCompleted
    };
}
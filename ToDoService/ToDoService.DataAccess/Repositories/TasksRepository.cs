using Microsoft.EntityFrameworkCore;
using ToDoService.DataAccess.Contexts;
using ToDoService.DataAccess.Entities;

namespace ToDoService.DataAccess.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly ToDoDbContext _dbContext;

    public TasksRepository(ToDoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<ToDoTaskEntity>> GetTasks()
    {
        return _dbContext.Tasks.ToListAsync();
    }

    public Task<ToDoTaskEntity> GetById(int id)
    {
        var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task Create(ToDoTaskEntity task)
    {
        _dbContext.Tasks.Add(task);
        _dbContext.SaveChanges();
        return Task.CompletedTask;
    }

    public async Task Update(int id, string description, bool isCompleted)
    {
        var foundTask = await GetById(id);
        if (foundTask == null) return;
        foundTask.Description = description;
        foundTask.IsCompleted = isCompleted;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var task = await GetById(id);
        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync();
    }
}
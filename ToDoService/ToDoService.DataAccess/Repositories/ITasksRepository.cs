using ToDoService.DataAccess.Entities;

namespace ToDoService.DataAccess.Repositories;

public interface ITasksRepository
{
    Task<List<ToDoTaskEntity>> GetTasks();
    Task<ToDoTaskEntity> GetById(int id);
    Task Create(ToDoTaskEntity task);
    Task Update(int id, string description, bool isCompleted);
    Task Delete(int id);
}
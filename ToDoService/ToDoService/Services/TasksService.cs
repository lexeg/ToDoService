using AutoMapper;
using ToDoService.DataAccess.Entities;
using ToDoService.DataAccess.Repositories;
using ToDoService.Models;

namespace ToDoService.Services;

public class TasksService : ITasksService
{
    private readonly ITasksRepository _tasksRepository;
    private readonly IMapper _mapper;

    public TasksService(ITasksRepository tasksRepository, IMapper mapper)
    {
        _tasksRepository = tasksRepository;
        _mapper = mapper;
    }

    public async Task<List<ToDoTask>> GetTasks()
    {
        var tasks = (await _tasksRepository.GetTasks()).Select(t => _mapper.Map<ToDoTask>(t)).ToList();
        return tasks;
    }

    public async Task<ToDoTask> GetById(int id)
    {
        var entity = await _tasksRepository.GetById(id);
        return _mapper.Map<ToDoTask>(entity);
    }

    public Task Create(ToDoTask task) => _tasksRepository.Create(_mapper.Map<ToDoTaskEntity>(task));

    public Task Update(int id, UpdateToDoTask task) =>
        _tasksRepository.Update(id, task.Description, task.IsCompleted);

    public Task Delete(int id) => _tasksRepository.Delete(id);
}
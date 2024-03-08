using Microsoft.AspNetCore.Mvc;
using ToDoService.Models;
using ToDoService.Services;

namespace ToDoService.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;

    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpGet]
    public Task<List<ToDoTask>> GetTasks()
    {
        return _tasksService.GetTasks();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _tasksService.GetTaskById(id);
        if (task == null) return new NotFoundResult();
        return Ok(task);
    }

    [HttpPost]
    public Task CreateTask([FromBody]ToDoTask task)
    {
        return _tasksService.Create(task);
    }

    [HttpDelete("{id}")]
    public Task Delete(int id)
    {
        return _tasksService.Delete(id);
    }
}
using Microsoft.AspNetCore.Mvc;
using ToDoService.Messages;
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
        var task = await _tasksService.GetById(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public Task CreateTask([FromBody] ToDoTask task)
    {
        return _tasksService.Create(task);
    }

    [HttpPut("{id}")]
    public Task Update([FromRoute] int id, [FromBody] UpdateToDoTask task)
    {
        return _tasksService.Update(id, task);
    }

    [HttpDelete("{id}")]
    public Task Delete(int id)
    {
        return _tasksService.Delete(id);
    }

    [HttpPost("file")]
    public async Task<IActionResult> UploadFile([FromForm] LoadFileRequest request)
    {
        if (await _tasksService.UploadFile(request.File))
        {
            return Ok();
        }

        return BadRequest();
    }
}
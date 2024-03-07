using Microsoft.AspNetCore.Mvc;

namespace ToDoService.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    [HttpGet]
    public Task<List<string>> GetTasks()
    {
        return Task.FromResult(new List<string>
        {
            "задача 1",
            "задача 2",
            "задача 3"
        });
    }

    [HttpGet("{id}")]
    public Task<string> GetById(int id)
    {
        return Task.FromResult($"задача {id}");
    }
}
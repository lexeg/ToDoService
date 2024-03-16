using Microsoft.AspNetCore.Mvc;

namespace ToDoService.Messages;

public class LoadFileRequest
{
    [BindProperty(Name = "formFile")]
    public IFormFile File { get; set; }
}
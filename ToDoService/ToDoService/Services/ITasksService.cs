﻿using ToDoService.Models;

namespace ToDoService.Services;

public interface ITasksService
{
    Task<List<ToDoTask>> GetTasks();
    Task<ToDoTask> GetById(int id);
    Task Create(ToDoTask task);
    Task Update(int id, UpdateToDoTask task);
    Task Delete(int id);
    Task<bool> UploadFile(IFormFile file);
    Task<bool> UploadBinaryFile(string fileName, byte[] bytesData);
}
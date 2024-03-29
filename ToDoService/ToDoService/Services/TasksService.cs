﻿using AutoMapper;
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

    public async Task<bool> UploadFile(IFormFile file)
    {
        try
        {
            if (file.Length <= 0) return false;
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            await using var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create);
            await file.CopyToAsync(fileStream);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("File Copy Failed", ex);
        }
    }

    public async Task<bool> UploadBinaryFile(string fileName, byte[] bytesData)
    {
        try
        {
            if (bytesData.Length <= 0) return false;
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            await using var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
            await fileStream.WriteAsync(bytesData);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("File Copy Failed", ex);
        }
    }
}
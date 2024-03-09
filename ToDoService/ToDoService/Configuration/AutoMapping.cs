using AutoMapper;
using ToDoService.DataAccess.Entities;
using ToDoService.Models;

namespace ToDoService.Configuration;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<ToDoTaskEntity, ToDoTask>().ReverseMap();
    }
}
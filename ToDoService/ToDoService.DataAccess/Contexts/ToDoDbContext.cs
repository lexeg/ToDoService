using Microsoft.EntityFrameworkCore;
using ToDoService.DataAccess.Entities;

namespace ToDoService.DataAccess.Contexts;

public sealed class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<ToDoTaskEntity> Tasks { get; set; }
}
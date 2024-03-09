using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoService.Configuration;
using ToDoService.DataAccess.Contexts;
using ToDoService.DataAccess.Repositories;
using ToDoService.Services;

namespace ToDoService;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(
            _ => new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); }).CreateMapper());

        services.AddDbContext<ToDoDbContext>(optionsBuilder =>
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITasksRepository, TasksRepository>();
        services.AddScoped<ITasksService, TasksService>();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", () => "Hello World!");
            endpoints.MapControllers();
        });
    }
}
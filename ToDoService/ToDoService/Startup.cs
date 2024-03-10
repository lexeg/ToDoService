using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using ToDoService.Configuration;
using ToDoService.DataAccess.Contexts;
using ToDoService.DataAccess.Repositories;
using ToDoService.Services;

namespace ToDoService;

public class Startup
{
    // Set the limit to 256 MB
    private const int MaxLimit = 268435456;

    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<KestrelServerOptions>(options => { options.Limits.MaxRequestBodySize = MaxLimit; });

        services.Configure<FormOptions>(options =>
        {
            options.ValueLengthLimit = MaxLimit;
            options.MultipartBodyLengthLimit = MaxLimit;
            options.MultipartHeadersLengthLimit = MaxLimit;
        });

        services.AddSingleton(
            _ => new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); }).CreateMapper());

        services.AddDbContext<ToDoDbContext>(optionsBuilder =>
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITasksRepository, TasksRepository>();
        services.AddScoped<ITasksService, TasksService>();
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", () => "Hello World!");
            endpoints.MapControllers();
        });
    }
}
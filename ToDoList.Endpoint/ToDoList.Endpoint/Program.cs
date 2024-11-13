using FluentValidation;
using Serilog;
using ToDoList.Persistence.Repositories;
using ToDoList.Endpoint.Validators;
using ToDoList.Endpoint.Profiles;
using ToDoList.Endpoint.Services;
using ToDoList.Endpoint;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

// Add services to the container.

builder.Services.AddSingleton(Log.Logger);
builder.Services.AddAutoMapper(typeof(ToDoItemProfile));
builder.Services.AddScoped<IToDoListService, ToDoListService>();
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<ToDoItemValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();

using Microsoft.EntityFrameworkCore;
using Tasks.API.Data;
using Tasks.API.Data.Abstract;
using Tasks.API.Data.Concrete;
using Tasks.API.Services;
using FluentValidation;
using System.Reflection;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["SQLServerConnection"]));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddHostedService<TaskDeadlineCheckerBackgroundService>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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

app.Run();
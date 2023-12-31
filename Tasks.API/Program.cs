﻿using Microsoft.EntityFrameworkCore;
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
            options.UseSqlite(builder.Configuration.GetSection("ConnectionStrings")["SQLiteConnection"]));

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

app.UseCors(policy =>
{
    policy.WithOrigins("https://localhost:7122", "http://localhost:5190")
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleWare>();

app.MapControllers();

app.Run();
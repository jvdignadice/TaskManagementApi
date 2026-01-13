using Microsoft.EntityFrameworkCore;
using System;
using taskmanagement.api.ms.domain.Database;
using taskmanagement.api.ms.domain.Interface;
using taskmanagement.api.ms.domain.Service;
using taskmanagement.api.ms.infrastructure.DependencyInjection;
using taskmanagement.api.ms.Interfaces;
using taskmanagement.api.ms.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaskManagementAppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddInfrastructureServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskManagementService, TaskManagementService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskManagementAppDbContext>();
    db.Database.EnsureCreated();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

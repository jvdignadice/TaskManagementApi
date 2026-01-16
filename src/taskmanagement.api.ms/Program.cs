using Microsoft.EntityFrameworkCore;
using taskmanagement.api.ms.domain.Database;
using taskmanagement.api.ms.domain.Interface;
using taskmanagement.api.ms.infrastructure.DependencyInjection;
using taskmanagement.api.ms.Interfaces;
using taskmanagement.api.ms.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaskManagementAppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddScoped<ITaskDbContext>(provider =>
    provider.GetRequiredService<TaskManagementAppDbContext>());

builder.Services.AddInfrastructureServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskManagementService, TaskManagementService>();
/* 
Add CORS service
for the sake of this assessment I will not mask the Front end
url, please modify the url depending of what it makes on your local ("http://localhost:5175")
*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5175") 
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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
app.UseCors("AllowFrontend");
app.UseAuthorization();

app.MapControllers();

app.Run();

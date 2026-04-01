using Microsoft.EntityFrameworkCore;
using EWTS.Infrastructure.Persistence;
using EWTS.Domain.Interfaces;
using EWTS.Infrastructure.Repositories;
using EWTS.Application.Interfaces;
using EWTS.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); ← keep this commented for now

app.UseAuthorization();

app.MapControllers();

app.Run();
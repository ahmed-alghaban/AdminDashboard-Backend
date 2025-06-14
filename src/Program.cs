using Microsoft.EntityFrameworkCore;
using AdminDashboard.src.Configs;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

app.Run();

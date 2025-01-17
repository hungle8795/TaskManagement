using Microsoft.EntityFrameworkCore;
using Serilog;
using Server.Data;

var builder = WebApplication.CreateBuilder(args);

// CORS config
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7273") // Blazor client of URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // if using cookie or authen
    });
});

// Add services to the container.
builder.Services.AddControllers();

// Serilog config
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/tasklog.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

// Change default logging wo serilog
builder.Host.UseSerilog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=tasksmanagement.db"));

var app = builder.Build();

// use CORS
app.UseCors("AllowBlazorClient");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();

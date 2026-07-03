using DSW2026Ej15.Api.Middleware;
using DSW2026Ej15.Data;
using DSW2026Ej15.Domain.Interface;
using Microsoft.EntityFrameworkCore;
namespace DSW2026Ej15.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=DSW2026Ej15;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;";

        builder.Services.AddDbContext<DSW2026Ej15DbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();
        builder.Services.AddScoped<IPersistence, PersistenceEf>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseAuthorization();
        app.MapGet("/health-check", () => Results.Ok("Healthy"));
        app.MapControllers();
        app.Run();
    }
}
